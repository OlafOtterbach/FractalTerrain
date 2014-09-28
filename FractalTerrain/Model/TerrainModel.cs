/// <summary>Definition of the class TerrainModelData.</summary>
/// <author>Olaf Otterbach</author>

using System;
using System.Windows.Media.Media3D;

namespace FractalTerrain.Model
{
   public class TerrainModel
   {
      public TerrainModel( ITerrainModelDataGenerator terrainGenerator, IAppleManDataGenerator appleManGenerator )
      {
         m_terrainGenerator = terrainGenerator;
         m_appleManGenerator = appleManGenerator;
         m_mapSize = 3;
         m_minimalSize = 0.1; 
         m_minimalStartPosition = -2.0;
         m_maximalStartPosition =  0.9;
         m_maximalEndPosition = m_maximalStartPosition + m_minimalSize;
         m_appleManXStartPosition = -1.5;
         m_appleManYStartPosition = -1.5;
         m_appleManSize = MaximalStartPosition - MinimalStartPosition;
      }

      public TerrainModelData TerrainModelData { get; private set; }

      public AppleManData AppleManData { get; private set; }

      public int MapSize
      {
         get
         {
            return m_mapSize;
         }
         set
         {
            m_mapSize = value;
            CleanUp();
         }
      }

      public double AppleManXStartPosition
      {
         get
         {
            return m_appleManXStartPosition;
         }
         set
         {
            m_appleManXStartPosition = value;
            CleanUp();
         }
      }

      public double AppleManYStartPosition
      {
         get
         {
            return m_appleManYStartPosition;
         }
         set
         {
            m_appleManYStartPosition = value;
            CleanUp();
         }
      }

      public double AppleManSize
      {
         get
         {
            return m_appleManSize;
         }
         set
         {
            m_appleManSize = value;
            CleanUp();
         }
      }


      public double MinimalSize
      {
         get
         {
            return m_minimalSize;
         }
         set
         {
            m_minimalSize = value;
            CleanUp();
         }
      }

      public double MinimalStartPosition 
      { 
         get
         {
            return m_minimalStartPosition;
         }
         set
         {
            m_minimalStartPosition = value;
            CleanUp();
         }
      }

      public double MaximalStartPosition
      {
         get
         {
            return m_maximalStartPosition;
         }
         set
         {
            m_maximalStartPosition = value;
            CleanUp();
         }
      }

      public void Update()
      {
         AppleManData = m_appleManGenerator.Create(MapSize, AppleManXStartPosition, AppleManYStartPosition, AppleManSize );
         TerrainModelData = m_terrainGenerator.Create(AppleManData);
      }

      public void UpdateAppleMan()
      {
         AppleManData = m_appleManGenerator.Create(MapSize, AppleManXStartPosition, AppleManYStartPosition, AppleManSize);
      }

      public void UpdateTerrain()
      {
         TerrainModelData = m_terrainGenerator.Create(AppleManData);
      }

      private void CleanUp()
      {
         MinimalSizeMaximalRule();
         MapSizeTrimmRule();
         MinimalStartPositionLessThanMaximalStartPositionRule();
         AppleManStartPositionClippingRule();
         AppleManSizeClippingRule();
      }

      private void MapSizeTrimmRule()
      {
         m_mapSize = ( 2 << ( (int)Math.Log(m_mapSize, 2) - 1 ) ) + 1;
      }

      private void AppleManStartPositionClippingRule()
      {
         if( m_appleManXStartPosition < m_minimalStartPosition ) m_appleManXStartPosition = m_minimalStartPosition;
         if( m_appleManXStartPosition > m_maximalStartPosition ) m_appleManXStartPosition = m_maximalStartPosition;
         if( m_appleManYStartPosition < m_minimalStartPosition ) m_appleManYStartPosition = m_minimalStartPosition;
         if( m_appleManYStartPosition > m_maximalStartPosition ) m_appleManYStartPosition = m_maximalStartPosition;
      }

      private void AppleManSizeClippingRule()
      {
         var deltaX = m_maximalEndPosition - AppleManXStartPosition;
         if( deltaX < m_appleManSize )
         {
            m_appleManSize = deltaX;
         }
         var deltaY = m_maximalEndPosition - AppleManXStartPosition;
         if( deltaY < m_appleManSize )
         {
            m_appleManSize = deltaY;
         }
      }

      private void MinimalStartPositionLessThanMaximalStartPositionRule()
      {
         if( m_maximalEndPosition < m_minimalStartPosition )
         {
            var help = m_minimalStartPosition;
            m_minimalStartPosition = m_maximalStartPosition;
            m_maximalStartPosition = help;
         }
      }

      private void MinimalSizeMaximalRule()
      {
         const double epsilon = 0.1;
         if( m_minimalSize < epsilon ) m_minimalSize = epsilon;
      }

      private ITerrainModelDataGenerator m_terrainGenerator;

      private IAppleManDataGenerator m_appleManGenerator;

      private int m_mapSize;

      private double m_minimalStartPosition;

      private double m_maximalStartPosition;

      private double m_maximalEndPosition;

      private double m_appleManXStartPosition;

      private double m_appleManYStartPosition;

      private double m_appleManSize;

      private double m_minimalSize;
   }
}
