using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cnab
{
    /// <summary>
    /// Unit tests against Methods inside the Helper class.
    /// </summary>
    public class Helper
    {

        [Fact]
        public async Task ReadFormFileAsyncShouldReturnNullWhenFileIsNull()
        {
            // Arrange
            var file = new Mock<IFormFile>();

            // Act
            var result = await Api.Utils.Helper.ReadFormFileAsync(file.Object);

            // Assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ReadFormFileAsyncShouldReturnNullWhenFileLengthIs0()
        {
            // Arrange
            var file = new Mock<IFormFile>();
            file.Setup(x => x.Length).Returns(0);

            // Act
            var result = await Api.Utils.Helper.ReadFormFileAsync(file.Object);

            // Assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ReadFormFileAsyncShouldReturnStringWhenFileHaveContent()
        {
            // Arrange
            var file = CreateFile("mock/txt", GetContent(), "txt");

            // Act
            var result = await Api.Utils.Helper.ReadFormFileAsync(file);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task IsTxtExtensionShouldReturnTruWhenFileMatchExtension()
        {
            // Arrange
            var file = CreateFile("mock/txt", GetContent(), "txt");

            // Act
            var result = await Task.FromResult(Api.Utils.Helper.IsTxtExtension(file));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task IsTxtExtensionShouldReturnFalseWhenDontMatchExtension()
        {
            // Arrange
            var file = CreateFile("mock/txt", GetContent(), "csv");

            // Act
            var result =  await Task.FromResult(Api.Utils.Helper.IsTxtExtension(file));

            // Assert
            result.Should().BeFalse();
        }


        #region Helper Methods
        public static IFormFile CreateFile(string contentType, string content, string extension)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            var file = new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: "CNAB." + $"{extension}"
                )
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
            };
            return file;
        }

        public static string GetContent()
        {
          const string content = "3201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO       "+"\r\n" +
                        "5201903010000013200556418150633123****7687145607MARIA JOSEFINALOJA DO Ó -MATRIZ" +"\r\n" +
                        "3201903010000012200845152540736777****1313172712MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n"+
                        "2201903010000011200096206760173648****0099234234JOÃO MACEDO   BAR DO JOÃO       "+"\r\n" +
                        "1201903010000015200096206760171234****7890233000JOÃO MACEDO   BAR DO JOÃO       "+"\r\n" +
                        "2201903010000010700845152540738723****9987123333MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n" +
                        "2201903010000050200845152540738473****1231231233MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n" +
                        "3201903010000060200232702980566777****1313172712JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n" +
                        "1201903010000020000556418150631234****3324090002MARIA JOSEFINALOJA DO Ó -MATRIZ"+"\r\n" +
                        "5201903010000080200845152540733123****7687145607MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n" +
                        "2201903010000010200232702980568473****1231231233JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n" +
                        "3201903010000610200232702980566777****1313172712JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n" +
                        "4201903010000015232556418150631234****6678100000MARIA JOSEFINALOJA DO Ó -FILIAL"+"\r\n" +
                        "8201903010000010203845152540732344****1222123222MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n" +
                        "3201903010000010300232702980566777****1313172712JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n" +
                        "9201903010000010200556418150636228****9090000000MARIA JOSEFINALOJA DO Ó -MATRIZ"+"\r\n" +
                        "4201906010000050617845152540731234****2231100000MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n" +
                        "2201903010000010900232702980568723****9987123333JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n" +
                        "8201903010000000200845152540732344****1222123222MARCOS PEREIRAMERCADO DA AVENIDA"+"\r\n"+
                        "2201903010000000500232702980567677****8778141808JOSÉ COSTA    MERCEARIA 3 IRMÃOS"+"\r\n"+
                        "3201903010000019200845152540736777****1313172712MARCOS PEREIRAMERCADO DA AVENIDA";
            return content;
        }
        #endregion
    }
}