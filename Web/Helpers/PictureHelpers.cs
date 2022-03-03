using Entities;

namespace Web.Helpers
{
    public static class PictureHelpers
    {
        public static string getCoverPhoto(int? coverId,List<ProductPicture> productPictures)
        {
            foreach (var picture in productPictures)
            {
                if(picture.PictureId == coverId)
                {
                    return picture.Picture.Url;
                }
            }
            return "";
        }
    }
}
