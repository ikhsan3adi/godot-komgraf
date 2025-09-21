namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class BentukDasar: RefCounted, IDisposable
{
    private Primitif _primitif = new Primitif();
    
    public List<Vector2> Margin(){
        return CheckPrimitifAndCall(() => _primitif.Margin());
    }    
    public List<Vector2> Persegi(float x, float y, float ukuran)
    {
        return CheckPrimitifAndCall(() => _primitif.Persegi(x, y, ukuran));
    }

    public List<Vector2> PersegiPanjang(float x, float y, float panjang, float lebar)
    {
        return CheckPrimitifAndCall(() => _primitif.PersegiPanjang(x, y, panjang, lebar));
    }


    public List<Vector2> SegitigaSiku(Vector2 titikAwal, int alas, int tinggi)
    {
        return CheckPrimitifAndCall(() => _primitif.SegitigaSiku(titikAwal, alas, tinggi));
    }

    public List<Vector2> TrapesiumSiku(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
    {
        return CheckPrimitifAndCall(() => _primitif.TrapesiumSiku(titikAwal, panjangAtas, panjangBawah, tinggi));
    }

    public List<Vector2> TrapesiumSamaKaki(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
    {
        return CheckPrimitifAndCall(() => _primitif.TrapesiumSamaKaki(titikAwal, panjangAtas, panjangBawah, tinggi));
    }

    public List<Vector2> JajarGenjang(Vector2 titikAwal, int alas, int tinggi, int jarakBeda)
    {
        return CheckPrimitifAndCall(() => _primitif.JajarGenjang(titikAwal, alas, tinggi, jarakBeda));
    }

        // New Circle and Ellipse Methods
    public List<Vector2> Lingkaran(Vector2 titikAwal, int radius)
    {
        return CheckPrimitifAndCall(() => _primitif.CircleMidPoint((int)titikAwal.X, (int)titikAwal.Y, radius));
    }

    public List<Vector2> Elips(Vector2 titikAwal, int radiusX, int radiusY)
    {
        return CheckPrimitifAndCall(() => _primitif.EllipseMidpoint((int)titikAwal.X, (int)titikAwal.Y, radiusX, radiusY));
    }

    private List<Vector2> CheckPrimitifAndCall(Func<List<Vector2>> action)
    {
        List<Vector2> checkResult = NodeUtils.CheckPrimitif(_primitif);
        if (checkResult != null) return checkResult;
        return action();
    }

    public List<Vector2> Polygon(List<Vector2> points)
    {
        List<Vector2> checkResult = NodeUtils.CheckPrimitif(_primitif);
        if (checkResult != null) return checkResult;

        List<Vector2> polygonPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            int nextIndex = (i + 1) % points.Count; // Wrap around to the first point for the last line
            polygonPoints.AddRange(_primitif.LineBresenham(points[i].X, points[i].Y, points[nextIndex].X, points[nextIndex].Y));
        }
        return polygonPoints;
    }

    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected new virtual void Dispose(bool disposing) // Fungsi Dispose() yang sebenarnya
    {
        if (disposing)
        {
            NodeUtils.DisposeAndNull(_primitif, "_primitif");
        }
    }
}
