using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MobyDick
{
    class Program
    {
        #region Fields
        // Get current user desktop path.
        private static string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string _savePath = _desktopPath + "/mobydick.xml";
        private static string _endPointAddress = "http://www.gutenberg.org/files/2701/2701-0.txt";
        // Download text from url
        private static string contents;
        #endregion
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Lütfen bekleyin XML dosyası oluşturuluyor...");

                
                using (var wc = new System.Net.WebClient())
                    contents = wc.DownloadString(_endPointAddress);

                // Create and write to mobydick.txt file 
                //File.WriteAllText(_desktopPath + "mobydick.txt", contents);

                // Get most common words from text file
                var orderedWords = contents
                                  .Split(' ')
                                  .GroupBy(x => x)
                                  .Select(x => new
                                  {
                                      Word = x.Key,
                                      Count = x.Count()
                                  })
                                  .OrderByDescending(x => x.Count).ToList();

                // Delete file if exist
                if (File.Exists(_savePath))
                {
                    File.Delete(_savePath);
                }

                XDocument doc = new XDocument();
                XElement rootElement = new XElement("words");
                orderedWords.ForEach(i =>
                                {
                                    var element = new XElement("word",
                                    new XAttribute("text", i.Word),
                                    new XAttribute("count", i.Count));
                                    rootElement.Add(element);
                                });
                doc.Add(rootElement);
                doc.Save(_savePath);

                Console.WriteLine("Dosya Başarılı bir şekilde oluşturuldu.Masaüstü dizininizde 'mobydick.xml' adlı dosyayı inceleyiniz.");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

