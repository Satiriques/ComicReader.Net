// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
public partial class ComicInfo
{
    public string Title { get; set; }

    public string Series { get; set; }

    [XmlIgnore]
    public IEnumerable<string> SeriesList
    {
        get { return Series.Split(',').Select(x => x.Trim()); }
        set { Series = string.Join(",", value); }
    }

    public string Summary { get; set; }

    public string Writer { get; set; }

    public string Penciller { get; set; }

    public string Inker { get; set; }

    public string Letterer { get; set; }

    public string CoverArtist { get; set; }

    public string Editor { get; set; }

    public string Publisher { get; set; }

    public string Genre { get; set; }

    [XmlIgnore]
    public IEnumerable<string> GenreList
    {
        get { return Genre.Split(',').Select(x => x.Trim()); }
        set { Genre = string.Join(",", value); }
    }

    public string Web { get; set; }

    public string LanguageISO { get; set; }

    public string Translator { get; set; }

    public string AgeRating { get; set; }

    public string Manga { get; set; }

    public string Characters { get; set; }

    [XmlIgnore]
    public IEnumerable<string> CharactersList
    {
        get { return Characters.Split(',').Select(x => x.Trim()); }
        set { Characters = string.Join(",", value); }
    }

    public int PageCount { get; set; }

    [System.Xml.Serialization.XmlArrayItem("Page", IsNullable = false)]
    public ComicInfoPage[] Pages { get; set; }
}

[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
public partial class ComicInfoPage
{
    [System.Xml.Serialization.XmlAttribute()]
    public byte Image { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public uint ImageSize { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public ushort ImageWidth { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public ushort ImageHeight { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public string Key { get; set; }
}