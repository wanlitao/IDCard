using System;

namespace IDCard.Reader
{
    public abstract class IDCardReader : IIDCardReader
    {
        public IDCardActionResult ReadBaseInfo()
        {
            throw new NotImplementedException();
        }

        public IDCardActionResult ReadBaseInfo(string fileDirectory)
        {
            throw new NotImplementedException();
        }

        public IDCardActionResult ReadBaseTextPhotoInfo()
        {
            throw new NotImplementedException();
        }

        public IDCardActionResult ReadBaseTextPhotoInfo(string fileDirectory)
        {
            throw new NotImplementedException();
        }

        public IDCardActionResult ReadNewAddressInfo()
        {
            throw new NotImplementedException();
        }

        public IDCardActionResult ReadNewAddressInfo(string fileDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
