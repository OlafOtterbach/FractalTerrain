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
         m_appleManMinimalSize = 0.0005; 
         m_appleManMinimalPosition = -2.0;
         m_appleManMaximalPosition =  2.0;
         m_appleManXStartPosition = -1.5;
         m_appleManYStartPosition = -1.5;
         m_appleManSize = m_appleManMaximalPosition - m_appleManMinimalPosition;
      }

      public TerrainModelData TerrainModelData { get; set; }

      public AppleManData AppleManData { get; set; }

      public int MapSize
      {
         get
         {
            return m_mapSize;
         }
         set
         {
            m_mapSize = value;
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
            if( m_appleManXStartPosition < AppleManMinimalPosition ) m_appleManXStartPosition = AppleManMinimalPosition;
            if( m_appleManXStartPosition > AppleManMaximalPosition - AppleManSize ) m_appleManXStartPosition = AppleManMaximalPosition - AppleManSize;
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
            if( m_appleManYStartPosition < AppleManMinimalPosition ) m_appleManYStartPosition = AppleManMinimalPosition;
            if( m_appleManYStartPosition > AppleManMaximalPosition - AppleManSize ) m_appleManYStartPosition = AppleManMaximalPosition - AppleManSize;
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
            var middleX = AppleManXStartPosition + m_appleManSize / 2.0;
            var middleY = AppleManYStartPosition + m_appleManSize / 2.0;
            
            m_appleManSize = value;

            var maximalSize = AppleManMaximalPosition - AppleManMinimalPosition;
            if( m_appleManSize > maximalSize ) m_appleManSize = maximalSize;
            if( m_appleManSize < AppleManMinimalSize ) m_appleManSize = AppleManMinimalSize;
            AppleManXStartPosition = middleX - AppleManSize / 2.0;
            AppleManYStartPosition = middleY - AppleManSize / 2.0;
         }
      }


      public double AppleManMinimalSize
      {
         get
         {
            return m_appleManMinimalSize;
         }
         set
         {
            m_appleManMinimalSize = value;
            CleanUp();
         }
      }

      public double AppleManMinimalPosition 
      { 
         get
         {
            return m_appleManMinimalPosition;
         }
         set
         {
            m_appleManMinimalPosition = value;
            CleanUp();
         }
      }

      public double AppleManMaximalPosition
      {
         get
         {
            return m_appleManMaximalPosition;
         }
         set
         {
            m_appleManMaximalPosition = value;
            CleanUp();
         }
      }

      public void Update()
      {
         UpdateAppleMan();
         UpdateTerrain();
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
         if( m_appleManXStartPosition < m_appleManMinimalPosition ) m_appleManXStartPosition = m_appleManMinimalPosition;
         if( m_appleManXStartPosition > m_appleManMaximalPosition ) m_appleManXStartPosition = m_appleManMaximalPosition;
         if( m_appleManYStartPosition < m_appleManMinimalPosition ) m_appleManYStartPosition = m_appleManMinimalPosition;
         if( m_appleManYStartPosition > m_appleManMaximalPosition ) m_appleManYStartPosition = m_appleManMaximalPosition;
      }

      private void AppleManSizeClippingRule()
      {
         var deltaX = m_appleManMaximalPosition - AppleManXStartPosition;
         if( deltaX < m_appleManSize )
         {
            m_appleManXStartPosition = m_appleManMaximalPosition - m_appleManSize;
            if( m_appleManXStartPosition < m_appleManMinimalPosition )
            {
               m_appleManXStartPosition = m_appleManMinimalPosition;
               m_appleManSize = m_appleManMaximalPosition - AppleManXStartPosition;
            }
         }
         var deltaY = m_appleManMaximalPosition - AppleManYStartPosition;
         if( deltaY < m_appleManSize )
         {
            m_appleManYStartPosition = m_appleManMaximalPosition - m_appleManSize;
            if( m_appleManYStartPosition < m_appleManMinimalPosition )
            {
               m_appleManYStartPosition = m_appleManMinimalPosition;
               m_appleManSize = m_appleManMaximalPosition - AppleManYStartPosition;
            }
         }
      }

      private void MinimalStartPositionLessThanMaximalStartPositionRule()
      {
         if( m_appleManMaximalPosition < m_appleManMinimalPosition )
         {
            var help = m_appleManMinimalPosition;
            m_appleManMinimalPosition = m_appleManMaximalPosition;
            m_appleManMaximalPosition = help;
         }
      }

      private ITerrainModelDataGenerator m_terrainGenerator;

      private IAppleManDataGenerator m_appleManGenerator;

      private int m_mapSize;

      private double m_appleManMinimalPosition;

      private double m_appleManMaximalPosition;

      private double m_appleManXStartPosition;

      private double m_appleManYStartPosition;

      private double m_appleManSize;

      private double m_appleManMinimalSize;
   }
}
