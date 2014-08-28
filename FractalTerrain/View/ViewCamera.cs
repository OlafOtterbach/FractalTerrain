/// <summary>Definition of the class ViewCamera.</summary>
/// <author>Olaf Otterbach</author>
/// <start>13.04.2014</start>
/// <state>17.04.2014</state>

using System.Windows.Media.Media3D;

namespace FractalTerrain
{
   public class ViewCamera
   {
      public ViewCamera()
      {
         SetCamera( 0.0, 45.0, 300.0 );
         Target = new Point3D();
         NearPlane = 1.0;
      }

      public double AngleAxisEz { get; set; }

      public double AngleAxisEy { get; set; }

      public double Distance { get; set; }

      public double NearPlane { get; set; }

      public Point3D Target { get; set; }

      public Matrix3D Offset 
      { 
         get
         {
            var alpha = AngleAxisEz.DegToRad();
            var beta = AngleAxisEy.DegToRad();
            var rotAlpha = Matrix3DExtensions.CreateRotationXY(alpha);
            var rotBeta = Matrix3DExtensions.CreateRotationYZ(-beta);
            var translation = Matrix3DExtensions.CreateTranslation( new Point3D( 0.0, -Distance, 0.0 ) );
            var targetTranslation = Matrix3DExtensions.CreateTranslation(Target);
            var offset = translation * rotBeta * rotAlpha * targetTranslation;
            return offset;
         }
      }

      public void SetCamera(double alpha, double beta, double distance )
      {
         AngleAxisEz = alpha;
         AngleAxisEy = beta;
         Distance = distance;
      }


      public void MoveTo(Point3D target)
      {
         Target = target;
      }


      public void OrbitXY(double degAlpha)
      {
         AngleAxisEz = ( AngleAxisEz + degAlpha );
         if( AngleAxisEy < 0.0 ) AngleAxisEy -= 360.0;
         if( AngleAxisEy > 360.0 ) AngleAxisEy -= 360.0;
      }


      public void OrbitYZ(double degAlpha)
      {
         AngleAxisEy = AngleAxisEy - degAlpha;
         if( AngleAxisEy < 0.0 ) AngleAxisEy = 0.0;
         if( AngleAxisEy > 60.0 ) AngleAxisEy = 70.0;
      }


      public void Zoom(double delta)
      {
         Distance += delta;
         if( Distance < 100.0 )
         {
            Distance = 100.0;
         }
         if( Distance > 1000.0 )
         {
            Distance = 1000.0;
         }
      }
   }
}
