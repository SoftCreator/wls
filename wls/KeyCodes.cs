using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace wls
{
    public class KeyCodes
    {
        private readonly XDocument _xCodes;

        private KeyCodes()
        {
            const string fileName = "KeyCodes.xml";
            _xCodes = XDocument.Load(fileName);
            if (_xCodes.Root == null)
                throw new Exception("File with key codes has incorrect format");
        }

        private static KeyCodes _instance;
        public static KeyCodes Instance { get { return _instance ?? (_instance = new KeyCodes()); } }

        private string GetKeyFromEnum(int keyCode)
        {
            return Enum.IsDefined(typeof(Keys), keyCode)
                ? string.Format("#{0} - {1}#",Enum.GetName(typeof (Keys), keyCode), keyCode)
                : string.Format("#?{0}#", keyCode);
        }

        private string GetKey(int keyCode, int? layout, string sectionName, bool shift)
        {

            var codesCollection = layout == null
                                      ? _xCodes.Root
                                               .Elements()
                                               .FirstOrDefault(r => r.Name == sectionName)
                                      : _xCodes.Root
                                               .Elements()
                                               .FirstOrDefault(
                                                   r => r.Name == sectionName
                                                        && r.FirstAttribute.Name == "id"
                                                        && r.FirstAttribute.Value == layout.Value.ToString(CultureInfo.InvariantCulture));
            if (codesCollection != null)
            {
                XElement keyXml = codesCollection
                    .Elements()
                    .FirstOrDefault(
                        r => r.Attribute(XName.Get("Code")).Value == keyCode.ToString(CultureInfo.InvariantCulture));
                if (keyXml != null)
                {
                    XName valueAttr = XName.Get(shift ? "ShiftValue" : "Value");
                    if (keyXml.Attribute(valueAttr) != null) 
                        return keyXml.Attribute(valueAttr).Value;
                }
            }
            return null;
        }

        public string GetKey(int keyCode, int layout, bool shift)
        {
            string key = GetKey(keyCode, layout, "KeyboardLayout", shift) ?? GetKey(keyCode, null, "BaseKeys", shift);
            if (shift && key == null)
            {
                key = GetKey(keyCode, layout, "KeyboardLayout", false) ?? GetKey(keyCode, null, "BaseKeys", false);
                if (key != null)
                    key = key.ToUpperInvariant();
            }
            return key ?? GetKeyFromEnum(keyCode);
        }

        public int[] IgnoreKeys()
        {
            var ignoreKeysSection = _xCodes.Root.Elements().FirstOrDefault(r => r.Name == "Ignore");
            if (ignoreKeysSection != null)
            {
                var codes = ignoreKeysSection.Elements(XName.Get("Code"));
                return codes.Select(r => int.Parse(r.Value.Trim())).ToArray();
            }
            return new int[0];
        }
    }
}