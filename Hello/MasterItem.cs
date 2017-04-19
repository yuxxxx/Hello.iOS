using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CoreLocation;

namespace info.yunnxx.Hello
{
    public class MasterItem: MapKit.MKAnnotation
    {
        public MasterItem(string title, float x, float y)
        {
            _Title = title;
            Pos = new PointF(x, y);
        }

        string _Title;
        PointF Pos;
        public override string Title => _Title;
        public override CLLocationCoordinate2D Coordinate => new CLLocationCoordinate2D(Pos.X, Pos.Y);

        public string Hello { get; set; }
        public string Language { get; set; }
        public int Locale { get; set; }

    }

    public class MasterList : IReadOnlyList<MasterItem>
    {
        List<MasterItem> Items;

        public MasterList()
        {
            Items = new List<MasterItem>
            {
                { "アメリカ合衆国", "Hello", "en-US", 39.999733f, -98.6785034f },
                { "日本", "こんにちは", "ja-JP", 38.28f, 140.46f },
                { "スペイン", "Hola", "es-ES", 41.27f, -3.21f },
                { "イタリア", "Ciao", "it-IT", 42.7631902f, 12.2515222f },
                { "フランス", "Bonjour", "fr-FR", 46.6487132f, 2.6215658f }
            };
        
        }

        public MasterItem this[int index] => Items[index];

        public int Count => Items.Count;

        public IEnumerator<MasterItem> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public static class MasterListEx
    {
        public static void Add(this IList<MasterItem> items, string title, string hello, string language, float x, float y)
        {
            items.Add(new MasterItem(title, x, y)
            {
                Hello = hello,
                Language = language,
            });
        }
    }

}
