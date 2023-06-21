namespace cesium.generator
{
    public class DataGenerator
    {
        public static double[] GenerateEllipse(double x1, double y1, double x2, double y2, int amountOfPoints)
        {
            double[] ellipsePoints = new double[2 * amountOfPoints];

            // Calculate the center point
            double centerX = (x1 + x2) / 2;
            double centerY = (y1 + y2) / 2;

            // Calculate the distance between the two points
            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));


            // Calculate the semi-major and semi-minor axes
            double semiMajorAxis = Math.Abs(x2 - x1) * 0.75f;
            double semiMinorAxis = Math.Abs(y2 - y1) * 1.2f;

            // Calculate the angle increment between points
            double angleIncrement = 2 * Math.PI / amountOfPoints;
            double angle = 0;

            // Generate points on the ellipse
            for (int i = 0; i < amountOfPoints; i++)
            {
                double x = centerX + semiMajorAxis * Math.Cos(angle);
                double y = centerY + semiMinorAxis * Math.Sin(angle);

                ellipsePoints[2 * i] = x;
                ellipsePoints[2 * i + 1] = y;

                angle += angleIncrement;
            }

            return ellipsePoints;
        }

        public static double[] SplitLine(double x1, double y1, double x2, double y2)
        {
            double[] linePoints = new double[12];

            double deltaX = (x2 - x1) / 9;
            double deltaY = (y2 - y1) / 9;

            // Calculate the coordinates for all eight points
            for (int i = 0; i < 6; i++)
            {
                double x = x2 - i * deltaX;
                double y = y2 - i * deltaY;

                linePoints[2 * i] = x;
                linePoints[2 * i + 1] = y;
            }

            // Return an array containing points 2, 3, 4, and 5
            double[] result = new double[8];
            Array.Copy(linePoints, 2, result, 0, 8);

            return result;
        }
    }
}
