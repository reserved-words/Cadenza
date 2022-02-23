﻿using Cadenza.Domain;

namespace Cadenza.Core.SystemInfo;

public class OverrideViewModel
{
    public OverrideViewModel(LibrarySource source, ItemPropertyUpdate model)
    {
        Source = source;
        Id = model.Id;
        Item = model.Item;
        ItemType = model.ItemType;
        PropertyName = model.Property;
        OriginalValue = model.OriginalValue;
        OverrideValue = model.UpdatedValue;
    }

    public LibrarySource Source { get; set; }
    public LibraryItemType ItemType { get; set; }
    public string Id { get; set; }
    public string Item { get; set; }
    public ItemProperty PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string OverrideValue { get; set; }
}