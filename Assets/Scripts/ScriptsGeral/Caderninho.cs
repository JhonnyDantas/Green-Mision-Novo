using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caderninho : MonoBehaviour
{
    public GameObject caderninho, caderninhoMenu;
    public GameObject fecharTaskManager;
     public Text missoesBanco; // missões banco

    public int qtdBancos=5;
     
    void Start(){
        missoesBanco.text=" Conserte " + qtdBancos.ToString();
    } 

    public void PegouCaderno()
    {
        caderninho.SetActive(true);
        fecharTaskManager.SetActive(true);
    }

    public void LargouCaderno()
    {
        caderninho.SetActive(false);
        fecharTaskManager.SetActive(false);
        caderninhoMenu.SetActive(true);
    }

    public void MissaoBanco(){
        qtdBancos--;
         missoesBanco.text="Faltam " + qtdBancos.ToString() + " Bancos";
        Debug.Log(qtdBancos);
    }
}
