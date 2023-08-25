using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class webS : MonoBehaviour
{

    [System.Serializable]
    public struct estructuraDatosWeb
    {
        [System.Serializable]
        public struct registro
        {
            public string nombre;
            public float puntos;
        }
        public List<registro> registros;
    }

    public estructuraDatosWeb datos;

    PlayerNado player;
    public GameObject canvasRanking;

    public Transform tabla;
    public Transform nuevo;
    public GameObject plantillaRegistros;
    int cantidadRegistros = 5;

    public TMPro.TMP_InputField miNombre;

    private void Awake()
    {
        player = FindObjectOfType<PlayerNado>();
        canvasRanking.gameObject.SetActive(false);
    }

    private void Start()
    {
        //ProcesoInicialLectura();
    }

    [ContextMenu("Leer")]
    public void Leer(System.Action accionAlTerminar)
    {
        StartCoroutine(CorrutinaLeer(accionAlTerminar));
    }
    private IEnumerator CorrutinaLeer(System.Action accionAlTerminar)
    {
        UnityWebRequest web = UnityWebRequest.Get("https://pipasjourney.com/compartido/runpenguinswim.txt");
        yield return web.SendWebRequest();
        //esperamos a que vuelva..
        //Cunado vuelva:
        if (web.result != UnityWebRequest.Result.ConnectionError)
        {
            datos = JsonUtility.FromJson<estructuraDatosWeb>(web.downloadHandler.text);
            accionAlTerminar();
        }
        else
        {
            Debug.Log("ERROR");
        }

    }


    [ContextMenu("Escribir")]
    public void Escribir()
    {
        StartCoroutine(CorrutinaEscribir());
    }
    private IEnumerator CorrutinaEscribir()
    {
        WWWForm form = new WWWForm();
        form.AddField("archivo", "runpenguinswim.txt");
        form.AddField("texto", JsonUtility.ToJson(datos));


        UnityWebRequest web = UnityWebRequest.Post("https://pipasjourney.com/compartido/escribir.php", form);
        yield return web.SendWebRequest();
        //esperamos a que vuelva..
        //Cunado vuelva:
        if (web.result != UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }


    [ContextMenu("CrearTabla")]
    void CrearTabla()
    {
        for (int i = 0; i < cantidadRegistros; i++)
        {
            GameObject clon = Instantiate(plantillaRegistros, tabla);
            clon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -35);
            clon.name = i.ToString();
        }
    }


    [ContextMenu("PasarDatosATabla")]
    void PasarDatosATabla()
    {
        for (int i = 0; i < cantidadRegistros; i++)
        {
            tabla.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = datos.registros[i].nombre;
            tabla.GetChild(i).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = datos.registros[i].puntos.ToString();
        }
    }


    [ContextMenu("ComprobarNuevoRecord")]
    void ComprobarNuevoRecord()
    {
        if(player.GetRecord()> datos.registros[cantidadRegistros-1].puntos)
        {
            tabla.gameObject.SetActive(false);
            nuevo.gameObject.SetActive(true);
        }
        else
        {
            tabla.gameObject.SetActive(true);
            nuevo.gameObject.SetActive(false);
        }
    }

    [ContextMenu("insertar")]
    void InsertarNuevoRegistro()
    {
        //En que posicionse inserta
        for (int i = 0; i < cantidadRegistros; i++)
        {
            if(player.GetRecord() > datos.registros[i].puntos)
            {
                //INSERTO
                datos.registros.Insert(i, new estructuraDatosWeb.registro()
                {
                    nombre = miNombre.text,
                    puntos = player.GetRecord()
                });
                break; //salga del for
            }
        }
    }


    //CUANDO EL JUGADOR MUERE:
    public void ProcesoInicialLectura()
    {
        canvasRanking.gameObject.SetActive(true);
        Leer(CrearTablaPasarDatosComprobar);
    }
    void CrearTablaPasarDatosComprobar()
    {
        CrearTabla();
        PasarDatosATabla();
        ComprobarNuevoRecord();
    }
    //INPUT
    public void InputTermino()
    {
        if(miNombre.text != "")
        {
            canvasRanking.gameObject.SetActive(true);
            nuevo.gameObject.SetActive(false);
            tabla.gameObject.SetActive(true);
            Leer(InsertarYEscribir);
        }
    }
    void InsertarYEscribir()
    {
        InsertarNuevoRegistro();
        Escribir();
        PasarDatosATabla();
    }
}