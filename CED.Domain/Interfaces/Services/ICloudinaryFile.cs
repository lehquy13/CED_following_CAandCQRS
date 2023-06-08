namespace CED.Domain.Interfaces.Services;

public interface ICloudinaryFile
{
    string GetImage(string fileName);
    string UploadImage(string filePath);

}