﻿namespace eStore.Admin.Domain.Entities;

public class Mousepad : Goods
{
    public bool IsStitched { get; set; }
    public string TopMaterial { get; set; }
    public string BottomMaterial { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}