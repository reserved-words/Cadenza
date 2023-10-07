﻿//namespace Cadenza.Common.DTO;

//public class TagListDTO
//{
//    private const string Separator = "|";

//    private readonly List<string> _tags = new List<string>();

//    public ICollection<string> Tags => _tags;

//    public TagListDTO()
//    {
//    }

//    public TagListDTO(string tags)
//    {
//        if (tags != null)
//        {
//            _tags = tags?.Split(Separator).ToList();
//        }
//    }

//    public TagListDTO(ICollection<string> tags)
//    {
//        if (tags != null)
//        {
//            _tags = new List<string>(tags);
//        }
//    }

//    public TagListDTO(TagListDTO tagList)
//    {
//        if (tagList != null)
//        {
//            _tags = new List<string>(tagList.Tags);
//        }
//    }

//    public override string ToString()
//    {
//        return string.Join(Separator, Tags);
//    }

//    public override bool Equals(object obj)
//    {
//        if (obj is not TagListDTO list)
//            return false;

//        return Tags.ToString() == list.ToString();
//    }

//    public override int GetHashCode()
//    {
//        return Tags.GetHashCode();
//    }
//}