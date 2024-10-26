namespace Cn_LINQ_XML_SzerzokCikkekEgyszeru
{
	internal class Program
	{
		/// <summary>
		/// A DataSet objektum referenciáját tároló adattag.
		/// </summary>
		static dsXML dsAdatok;
		static void Main(string[] args)
		{
			// A DataSet objektum létrehozása.
			Létrehoz();
			var Adatok = dsAdatok;
			//
			// Lekérdezések.
			//
			// Szerzők és születési évek, születési év szerint növekvő sorrendben.
			// Azonos születési évüek név szerinti sorrenben.
			var E = from x in dsAdatok.dtSzerzők
							orderby x.SzületésiÉv ascending, x.Név
							select new { Név = x.Név, SzületésiÉv = x.SzületésiÉv };
			// 2900 Ft-nál drágább cikkek címe és ára cím szerint sorbarendezve.
			var F = from x in dsAdatok.dtCikkek
							where x.Ár > 2900
							orderby x.Cím
							select new { Cím = x.Cím, Ár = x.Ár };
			// Összes adat egy táblába.
			var G = from x in dsAdatok.dtSzerzők
							join y in dsAdatok.dtCikkek
								on x equals y.dtSzerzőkRow
							select new
							{
								Szerző = x.Név,
								SzületésiÉv = x.SzületésiÉv,
								Cím = y.Cím,
								URL = y.URL,
								Ár = y.Ár
							};
			// Egy szerző összes műve
		}
		/// <summary>
		/// Létrehozza a DataSet objektumot és feltölti adatokkal.
		/// </summary>
		private static void Létrehoz()
		{ // DataSet objektum létrehozása.
			dsAdatok = new dsXML();
			// Szerzők táblájának feltöltése.
			dsXML.dtSzerzőkRow sr1 =
				dsAdatok.dtSzerzők.AdddtSzerzőkRow(1, "Szellőző Eduárd", 1981);
			dsXML.dtSzerzőkRow sr2 =
				dsAdatok.dtSzerzők.AdddtSzerzőkRow(2, "Nyirkos Tenyér", 1975);
			dsXML.dtSzerzőkRow sr3 =
				dsAdatok.dtSzerzők.AdddtSzerzőkRow(3, "Merengő Margit", 1968);
			dsXML.dtSzerzőkRow sr4 =
				dsAdatok.dtSzerzők.AdddtSzerzőkRow(4, "Hétpróbás Gazember", 1968);
			// Cikkek táblájának feltöltése.
			dsAdatok.dtCikkek.AdddtCikkekRow(sr1, "Rácsok, rácsok, rácsok ...",
				"racsok.hu", 3500);
			dsAdatok.dtCikkek.AdddtCikkekRow(sr1, "Huzat van",
				"szellozo.hu", 2500);
			dsAdatok.dtCikkek.AdddtCikkekRow(sr3, "Álmok könyve",
				"almokkonyve.hu", 2500);
			dsAdatok.dtCikkek.AdddtCikkekRow(sr4, "Ártatlan vagyok",
				"artatlan-vagyok.hu", 2900);
			dsAdatok.dtCikkek.AdddtCikkekRow(sr4, "Mint a ma született bárány",
				"artatlan-vagyok.hu", 4100);
		}
	}
}
