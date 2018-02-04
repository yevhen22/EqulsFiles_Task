using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace FileEqualizer
{
    public class FileComparator
    {
        private string _path;
        private string[] _files;
        private MD5 _md5Hasher = MD5.Create();
        private Dictionary<string, string> _filesHashes = new Dictionary<string, string>();

        public string Path
        {
            get => _path;
            set
            {
                if (value != null) _path = value;
            }
        }

        /// <summary>
        /// ctor().
        /// </summary>
        /// <param name="path"></param>
        public FileComparator(string path)
        {
            _path = path;
        }


        public void GetFilesFromDirectory()
        {
            if (Directory.Exists(_path))
            {
                _files = Directory.GetFiles(_path);
                foreach (string s in _files)
                {
                    Console.WriteLine(s);
                }
            }
        }


        public void GetFilesHash()
        {
            foreach (var item in _files)
            {
                var hash = GetHash(item);
                _filesHashes.Add(item, hash);
                Console.WriteLine("Path = {0}, Hash = {1}", item, hash);
            }
        }

        /// <summary>
        /// Get hash code for files
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetHash(string filePath)
        {
            //For IDisposable
            using (FileStream fs = File.OpenRead(filePath))
            {
                return BitConverter.ToString(_md5Hasher.ComputeHash(fs));
            }
        }


        /// <summary>
        /// Show file names with equal content
        /// </summary>
        public void FindEqualFiles()
        {
            var intersect2 = _filesHashes.Where(i => _filesHashes.Any(t => t.Key != i.Key && t.Value == i.Value))
                                                .ToDictionary(i => i.Key, i => i.Value);
            foreach (var item in intersect2.Keys)
            {
                Console.WriteLine(item);
            }
        }
    }
}
