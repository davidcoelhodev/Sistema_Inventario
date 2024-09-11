using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Michsky.LSS
{
    [AddComponentMenu("Loading Screen Studio/LSS Manager")]
    public class LSS_Manager : MonoBehaviour
    {
        // Configurações
        [SerializeField]
        [Tooltip("Modo de carregamento da cena (Single ou Additive)")]
        private LoadingMode loadingMode;

        [SerializeField]
        [Tooltip("Nome do preset de carregamento")]
        private string presetName = "Default";

        [SerializeField]
        [Tooltip("Ativar gatilho de carregamento")]
        private bool enableTrigger;

        [SerializeField]
        [Tooltip("Carregar cena ao sair do gatilho")]
        private bool onTriggerExit;

        [SerializeField]
        [Tooltip("Carregar cena por tag do objeto")]
        private bool loadWithTag;

        [SerializeField]
        [Tooltip("Iniciar carregamento automaticamente ao iniciar o jogo")]
        private bool startLoadingAtStart;

        [SerializeField]
        [Tooltip("Tag do objeto a ser detectado")]
        private string objectTag;

        [SerializeField]
        [Tooltip("Nome da cena a ser carregada")]
        private string sceneName;

        // Áudio suave
        [Range(0, 10)]
        [Tooltip("Duração da transição suave de volume do áudio")]
        public float audioFadeDuration = 2;

        [Tooltip("Lista de fontes de áudio a serem manipuladas")]
        public List<AudioSource> audioSources = new List<AudioSource>();

        // Variáveis temporárias
        [Tooltip("Lista de telas de carregamento disponíveis")]
        public Object[] loadingScreens;

        [Tooltip("Índice de carregamento selecionado")]
        public int selectedLoadingIndex = 0;

        [Tooltip("Índice da tag selecionada")]
        public int selectedTagIndex = 0;

        // Eventos
        [Tooltip("Evento acionado no início do carregamento")]
        public UnityEvent onLoadingStart;

        [Tooltip("Objetos que não serão destruídos ao carregar novas cenas")]
        public List<GameObject> dontDestroyOnLoad = new List<GameObject>();

        // Apenas modo Additive
        [SerializeField] private List<string> loadedScenes = new List<string>();

#if UNITY_EDITOR
        public bool lockSelection = false;
#endif

        // Definição do modo de carregamento
        public enum LoadingMode { Single, Additive }

        void Start()
        {
            if (startLoadingAtStart && loadingMode == LoadingMode.Single)
            {
                LoadScene(sceneName);
            }
            else if (startLoadingAtStart && loadingMode == LoadingMode.Additive)
            {
                LoadSceneAdditive(sceneName);
            }
        }

        public void SetPreset(string styleName)
        {
            presetName = styleName;
        }

        // Carregar cena em modo Single
        public void LoadScene(string sceneName)
        {
            LSS_LoadingScreen.presetName = presetName;
            LSS_LoadingScreen.LoadScene(sceneName);

            foreach (var obj in dontDestroyOnLoad)
            {
                DontDestroyOnLoad(obj);
            }

            if (audioSources.Count > 0)
            {
                foreach (AudioSource asg in audioSources)
                {
                    var tempAS = asg.gameObject.AddComponent<LSS_AudioSource>();
                    tempAS.audioSource = asg;
                    tempAS.audioFadeDuration = audioFadeDuration;
                    tempAS.DoFadeOut();
                }
            }

            onLoadingStart.Invoke();
        }

        // Carregar cena em modo Additive
        public void LoadSceneAdditive(string sceneName)
        {
            LSS_LoadingScreen.LoadSceneAdditive(sceneName);
            loadedScenes.Add(SceneManager.GetSceneByName(sceneName).name);

            if (audioSources.Count > 0)
            {
                foreach (AudioSource asg in audioSources)
                {
                    var tempAS = asg.gameObject.AddComponent<LSS_AudioSource>();
                    tempAS.audioSource = asg;
                    tempAS.audioFadeDuration = audioFadeDuration;
                    tempAS.DoFadeOut();
                }
            }

            onLoadingStart.Invoke();
        }

        // Ações de gatilho para carregar cenas
        void DoTriggerActions()
        {
            LSS_LoadingScreen.presetName = presetName;

            if (loadingMode == LoadingMode.Single)
            {
                LSS_LoadingScreen.LoadScene(sceneName);
            }
            else if (loadingMode == LoadingMode.Additive)
            {
                LSS_LoadingScreen.LoadSceneAdditive(sceneName);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!enableTrigger || onTriggerExit) return;
            if (loadWithTag && other.gameObject.tag != objectTag) return;

            DoTriggerActions();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!enableTrigger || !onTriggerExit) return;
            if (loadWithTag && other.gameObject.tag != objectTag) return;

            DoTriggerActions();
        }
    }
}
