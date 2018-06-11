using System;
using System.IO;
using System.Text;
using GreenUtil.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.IO
{
    [TestClass]
    public class FileUtilTest
    {
        [TestMethod]
        public void WhenSavingMemoryStreamToFileThenFileShouldBeCreatedAndHaveMoreThanZeroBytes()
        {
            using (var memoryStream = new MemoryStream(new byte[] { 100, 42, 98, 210, 12, 13, 42, 64 }))
            {
                string filePath = "save_file_test.tmp";

                FileUtil.Save(memoryStream, filePath);

                var fileInfo = new FileInfo(filePath);

                Assert.AreNotEqual(0, fileInfo.Length);

                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void WhenNullMemoryStreamThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.Save(null, "save_file_test.tmp"));
        }

        [TestMethod]
        public void WhenNullPathThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.Save(new MemoryStream(), null));
        }

        [TestMethod]
        public void WhenReadingNonExistingFileThenShouldThrowException()
        {
            Assert.ThrowsException<FileNotFoundException>(() => FileUtil.GetEncodingFromBOM("Dummy\\NonExistingFile.txt"));
        }

        [TestMethod]
        public void WhenReadingNonExistingDirectoryThenShouldThrowException()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(() => FileUtil.GetEncodingFromBOM("\\NonExistingDirectory\\NonExistingFile.txt"));
        }
        
        [TestMethod]
        public void WhenReadingUTF8FileWithBOMThenEncondingShouldBeUTF8()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF8BOM.txt");

            Assert.AreEqual(Encoding.UTF8, encoding);
        }

        [TestMethod]
        public void WhenReadingUTF16BigEndianFileWithBOMThenEncondingShouldBeUTF16BigEndian()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF16BIGENDIAN.txt");

            Assert.AreEqual(Encoding.BigEndianUnicode, encoding);
        }

        [TestMethod]
        public void WhenReadingUTF16LittleEndianFileWithBOMThenEncondingShouldBeUTF16LittleEndian()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF16LITTLEENDIAN.txt");

            Assert.AreEqual(Encoding.Unicode, encoding);
        }

        // Para gerar arquivo UTF-7: EditPad Lite 7
        [TestMethod]
        public void WhenReadingUTF7WithBOMThenEncondingShouldBeUTF7()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF7.txt");

            Assert.AreEqual(Encoding.UTF7, encoding);
        }

        [TestMethod]
        public void WhenReadingUTF32WithBOMThenEncondingShouldBeUTF32()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF32.txt");

            Assert.AreEqual(Encoding.UTF32, encoding);
        }

        [TestMethod]
        public void WhenReadingUTF32BEWithBOMThenEncondingShouldBeUTF32BE()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF32BE.txt");

            Assert.AreEqual(new UTF32Encoding(true, true), encoding);
        }

        [TestMethod]
        public void WhenReadingUTF16UnicodeWithBOMThenEncondingShouldBeUTF16Unicode()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\UTF16UNICODE.txt");

            Assert.AreEqual(Encoding.Unicode, encoding);
        }

        [TestMethod]
        public void WhenReadingWindows1252FileWithBOMThenEncondingShouldBeDefault()
        {
            var encoding = FileUtil.GetEncodingFromBOM("Dummy\\WINDOWS1252.txt");

            Assert.AreEqual(Encoding.Default, encoding);
        }

        [TestMethod]
        public void WhenNullThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.GetEncodingFromBOM(null));
        }

        [TestMethod]
        public void WhenCheckingIfAvailableFileIsAvailableThenShouldReturnTrue()
        {
            Assert.IsTrue(FileUtil.IsFileAvailable("Dummy\\SAMPLE.txt"));
        }


        [TestMethod]
        public void WhenIsFileavailableWithNullThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.IsFileAvailable(null));
        }

        [TestMethod]
        public void WhenCheckingIfUnavailableIsAvailableThenShouldReturnFalse()
        {
            string filePath = "Dummy\\SAMPLE.txt";

            using (File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                Assert.IsFalse(FileUtil.IsFileAvailable(filePath));
            }
        }

        [TestMethod]
        public void WheAllValidCharsOnPathThenShouldReturnSamePath()
        {
            string filePath = "DummyFile.txt";

            string sanitizedFilePath = FileUtil.ReplaceInvalidPathChars(filePath);

            Assert.AreEqual(filePath, sanitizedFilePath);
        }

        [TestMethod]
        public void WhenInvalidCharsOnPathThenShouldReturnPathWithoutInvalidPaths()
        {
            string filePath = "DummyFile/?*<>|.txt";

            string sanitizedFilePath = FileUtil.ReplaceInvalidPathChars(filePath);

            Assert.AreEqual("DummyFile.txt", sanitizedFilePath);
        }

        [TestMethod]
        public void WhenInvalidCharsOnPathWithNullArgumentThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.ReplaceInvalidPathChars(null));
        }

        [TestMethod]
        public void WhenNullPathIsProvidedGetFileSizeShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileUtil.GetFileSize(null));
        }

        [TestMethod]
        public void WhenFileIsProvidedGetFileSizeShouldReturnFileSize()
        {
            Assert.AreEqual(97.66M, Math.Round(FileUtil.GetFileSize("Dummy\\UTF32.txt", Magnitude.KB), 2));
        }
    }
}
