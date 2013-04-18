using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace wls
{
    public class KeyCodes
    {
        private readonly XDocument _xCodes;

        private const int DefaultLayout = 1033;

        private KeyCodes()
        {
            const string fileName = "KeyCodes.xml";
            _xCodes = XDocument.Load(fileName);
        }

        private static KeyCodes _instance;
        public static KeyCodes Instance { get { return _instance ?? (_instance = new KeyCodes()); } }

        public string GetKey(int keyCode, int layout)
        {
            if (_xCodes.Root == null)
                throw new Exception("File with key codes has incorrect format");
            var codesCollection = _xCodes.Root
                                         .Elements()
                                         .FirstOrDefault(
                                             r => r.Name == "KeyboardLayout" && r.FirstAttribute.Name == "id" &&
                                                  r.FirstAttribute.Value == layout.ToString(CultureInfo.InvariantCulture));

            string key = "";
            if (codesCollection == null)
            {
                if (layout != DefaultLayout) return GetKey(keyCode, DefaultLayout);
                return key;
            }
            XElement keyXml = codesCollection
                .Elements().FirstOrDefault(r => r.Attribute(XName.Get("Code")).Value == keyCode.ToString(CultureInfo.InvariantCulture));
            
            if (keyXml != null) key = keyXml.Attribute(XName.Get("Value")).Value;
            return key;
        }
    }
}
