using MotoManager.Domain.Interfaces.Services;

namespace MotoManager.Infra.Services
{
    public class FileService : IFileService
    {
        private readonly string _caminhoArquivo = "c:\\motos\\doc";
        public string SaveDisk(string base64String)
        {
            var nomeArquivo = "doc-"+DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            if (string.IsNullOrEmpty(base64String))
                return null;

            try
            {
                var caminhoCompletoArquivo = Path.Combine(_caminhoArquivo, nomeArquivo);

                if (!Directory.Exists(_caminhoArquivo))
                    Directory.CreateDirectory(_caminhoArquivo);

                byte[] bytes = Convert.FromBase64String(base64String);

                File.WriteAllBytes(caminhoCompletoArquivo, bytes);

                return caminhoCompletoArquivo;
            }
            catch (Exception ex)
            {
                // LOG aqui
                return null;
            }
        }
    }
}
