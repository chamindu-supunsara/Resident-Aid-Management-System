using SITS.BNS.Entities.Interfaces;

namespace BNS.Services
{
    public class CommonService : ICommonService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CommonService(
            IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public bool TryParseDateTime(string dateStr, out DateTime result)
        {
            result = DateTime.MinValue;

            if (!string.IsNullOrEmpty(dateStr))
            {
                if (DateTime.TryParse(dateStr, out result))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid date format: " + dateStr);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("TargetDate is null or empty");
                return false;
            }
        }

        public DateTime? ParseDateTime(string dateStr, out DateTime result)
        {
            result = DateTime.MinValue;

            if (!string.IsNullOrEmpty(dateStr))
            {
                if (DateTime.TryParse(dateStr, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid date format: " + dateStr);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("TargetDate is null or empty");
                return null;
            }
        }

        public DateTime? GetDate(string strdate)
        {
            var date__ = DateTime.MinValue;
            var date_ = DateTime.MinValue;
            if (TryParseDateTime(strdate, out date__))
            {
                ParseDateTime(strdate, out date_);
                return date_;
            }
            else
            {
                return DateTime.MinValue;
            }
        }


        public async Task<string> GetBase64ImgUrl(string? ImagePath)
        {

            var _imgurl = "";
            if (!string.IsNullOrEmpty(ImagePath))
            {
                try
                {
                    if (File.Exists(ImagePath))
                    {
                        var _base64String = Convert.ToBase64String(File.ReadAllBytes(ImagePath));
                        _imgurl = "data:image/jpg;base64," + _base64String;
                        //D:\Innovation\Lion Brewery\LB-FMS-Web\SITS.LB.FMS\wwwroot\Images\Vehicles\Milage\0.jpeg
                        //https://localhost:44385/Images/Vehicles/Milage/0.JPEG

                        //var replace = ImagePath.Replace("\\", "/");
                        //var spilt = replace.Split("/wwwroot/");
                        //_imgurl = spilt[1];
                        //_imgurl = replace;
                    }
                    else
                    {
                        _imgurl = "";
                    }
                }
                catch (FileNotFoundException ex)
                {
                    _imgurl = "";
                }
                catch (DirectoryNotFoundException ex)
                {
                    _imgurl = "";
                }
                catch (IOException ex)
                {
                    _imgurl = "";
                }
                catch (Exception ex)
                {
                    _imgurl = "";
                }
            }
            else
            {
                _imgurl = "";
            }
            return _imgurl;
        }


        public async Task<bool> IsBase64(string? base64String)
        {

            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                // Handle the exception
            }
            return false;
        }

        public async Task<string> GetImgPath(string? ImagePath, int UserId, string? type, string? subtype)
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {

                try
                {
                    if (!ImagePath.Contains("Images"))
                    {
                        string[] imagestring = ImagePath.Split(",");


                        var extension_raw = imagestring[0].Replace("data:image/", "");
                        var extension = extension_raw.Replace(";base64", "");

                        var __folderPath = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                        var _folderPath = Path.Combine(__folderPath, type);
                        var folderPath = Path.Combine(_folderPath, subtype);

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string imageName = Guid.NewGuid() + "." + extension;
                        string imgPath = Path.Combine(folderPath, imageName);
                        if (imagestring != null && imagestring.Length > 1)
                        {
                            byte[] imageBytes = Convert.FromBase64String(imagestring[1]);



                            if (File.Exists(imgPath))
                            {
                                File.Delete(imgPath);
                            }


                            File.WriteAllBytes(imgPath, imageBytes);


                        }
                        else
                        {
                            imgPath = "";
                        }

                        return imgPath;
                    }
                    else
                    {

                        var __folderPath = Path.Combine(_hostEnvironment.WebRootPath, ImagePath);
                        return __folderPath;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            else
            {
                return "";
            }


        }

        DateTime ICommonService.GetDate(string strdate)
        {

            if (DateTime.TryParse(strdate, out DateTime result))
            {
                return result;
            }
            else
            {
                // Handle the case where the conversion fails
                throw new ArgumentException("Invalid date string format", nameof(strdate));
            }

        }

        public DateTime dateToday()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).Date;
        }

        public DateTime dateTimeToday()
        {
            return DateTime.Now.ToUniversalTime().AddHours(5).AddMinutes(30);
        }

        DateTime? ICommonService.dateToday()
        {
            throw new NotImplementedException();
        }

        DateTime? ICommonService.dateTimeToday()
        {
            throw new NotImplementedException();
        }
    }
}
