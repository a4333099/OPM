using System.Drawing;

namespace OPM.Core.RandomsMethod
{
    public interface IOPMRandom
    {
        string CreateRandomValue(int length, bool onlyNumber);
         
        string CreateRandomValueWithoutZero(int length, bool onlyNumber);

        RandomImage CreateRandomImage(string value, int imageWidth, int imageHeight, Color imageBGColor,
            Color imageTextColor1, Color imageTextColor2);
    }
}
