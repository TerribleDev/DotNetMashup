using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetMashup.Web.Model
{
    [XmlRoot(ElementName = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Url
    {
        [XmlElement(ElementName = "loc", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Loc { get; set; }

        [XmlElement(ElementName = "lastmod", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Lastmod { get; set; }

        [XmlElement(ElementName = "changefreq", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Changefreq { get; set; }

        [XmlElement(ElementName = "priority", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public string Priority { get; set; }
    }

    [XmlRoot(ElementName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Urlset
    {
        [XmlElement(ElementName = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public List<Url> Url { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}