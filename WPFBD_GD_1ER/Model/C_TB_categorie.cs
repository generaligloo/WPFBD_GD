#region Ressources extérieures
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace WPFBD_GD_1ER.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_TB_categorie
 {
  #region Données membres
  private int _ID_categorie;
  private string _Nom;
  private Int16 _Pegi;
  #endregion
  #region Constructeurs
  public C_TB_categorie()
  { }
  public C_TB_categorie(string Nom_, Int16 Pegi_)
  {
   Nom = Nom_;
   Pegi = Pegi_;
  }
  public C_TB_categorie(int ID_categorie_, string Nom_, Int16 Pegi_)
   : this(Nom_, Pegi_)
  {
   ID_categorie = ID_categorie_;
  }
  #endregion
  #region Accesseurs
  public int ID_categorie
  {
   get { return _ID_categorie; }
   set { _ID_categorie = value; }
  }
  public string Nom
  {
   get { return _Nom; }
   set { _Nom = value; }
  }
  public Int16 Pegi
  {
   get { return _Pegi; }
   set { _Pegi = value; }
  }
  #endregion
 }
}
