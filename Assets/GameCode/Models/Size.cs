﻿public class Size : ISize
{
    public int Height { get; set; }
    public int Width { get; set; }
}

public class LevelCoordinate : ISize
{
    public int Height { get; set; }
    public int Width { get; set; }

    public LevelCoordinate()
    { }

    public LevelCoordinate(int h, int w)
    {
        Height = h;
        Width = w;
    }

    public void CopyFrom(LevelCoordinate pos)
    {
        Height = pos.Height;
        Width = pos.Width;
    }

    public LevelCoordinate Clone()
    {
        return new LevelCoordinate
        {
            Height = this.Height,
            Width = this.Width
        };
    }
}