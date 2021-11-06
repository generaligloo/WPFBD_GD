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
 public class C_TB_edition
 {
  #region Données membres
  private int _ID_edition;
  private string _edi_nom;
  private DateTime _edi_dat;
  private string _edi_pdg_nom;
  private string _edi_pdg_prenom;
  #endregion
  #region Constructeurs
  public C_TB_edition()
  { }
  public C_TB_edition(string edi_nom_, DateTime edi_dat_, string edi_pdg_nom_, string edi_pdg_prenom_)
  {
   edi_nom = edi_nom_;
   edi_dat = edi_dat_;
   edi_pdg_nom = edi_pdg_nom_;
   edi_pdg_prenom = edi_pdg_prenom_;
  }
  public C_TB_edition(int ID_edition_, string edi_nom_, DateTime edi_dat_, string edi_pdg_nom_, string edi_pdg_prenom_)
   : this(edi_nom_, edi_dat_, edi_pdg_nom_, edi_pdg_prenom_)
  {
   ID_edition = ID_edition_;
  }
  #endregion
  #region Accesseurs
  public int ID_edition
  {
   get { return _ID_edition; }
   set { _ID_edition = value; }
  }
  public string edi_nom
  {
   get { return _edi_nom; }
   set { _edi_nom = value; }
  }
  public DateTime edi_dat
  {
   get { return _edi_dat; }
   set { _edi_dat = value; }
  }
  public string edi_pdg_nom
  {
   get { return _edi_pdg_nom; }
   set { _edi_pdg_nom = value; }
  }
  public string edi_pdg_prenom
  {
   get { return _edi_pdg_prenom; }
   set { _edi_pdg_prenom = value; }
  }
  #endregion
 }
}
