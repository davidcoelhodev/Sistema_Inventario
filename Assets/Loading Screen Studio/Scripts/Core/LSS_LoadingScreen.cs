using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
#if !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

namespace Michsky.LSS
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Loading Screen Studio/LSS Loading Screen")]
    public class LSS_LoadingScreen : MonoBehaviour
    {
        public static LSS_LoadingScreen instance = null;

        #region Customization
        [Tooltip("Tamanho da fonte do título")]
        public float titleSize = 50;

        [Tooltip("Tamanho da fonte da descrição")]
        public float descriptionSize = 28;

        [Tooltip("Tamanho da fonte da dica")]
        public float hintSize = 32;

        [Tooltip("Tamanho da fonte do status")]
        public float statusSize = 24;

        [Tooltip("Tamanho da fonte 'Pressione qualquer tecla'")]
        public float pakSize = 35;

        [Tooltip("Fonte do título")]
        public TMP_FontAsset titleFont;

        [Tooltip("Fonte da descrição")]
        public TMP_FontAsset descriptionFont;

        [Tooltip("Fonte da dica")]
        public TMP_FontAsset hintFont;

        [Tooltip("Fonte do status")]
        public TMP_FontAsset statusFont;

        [Tooltip("Fonte 'Pressione qualquer tecla'")]
        public TMP_FontAsset pakFont;

        [Tooltip("Cor do título")]
        public Color titleColor = Color.white;

        [Tooltip("Cor da descrição")]
        public Color descriptionColor = Color.white;

        [Tooltip("Cor da dica")]
        public Color hintColor = Color.white;

        [Tooltip("Cor do spinner")]
        public Color spinnerColor = Color.white;

        [Tooltip("Cor do status")]
        public Color statusColor = Color.white;

        [Tooltip("Cor da mensagem 'Pressione qualquer tecla'")]
        public Color pakColor = Color.white;

        [TextArea]
        [Tooltip("Texto do título da tela de carregamento")]
        public string titleObjText = "Title";

        [TextArea]
        [Tooltip("Texto da descrição da tela de carregamento")]
        public string titleObjDescText = "Description";

        [TextArea]
        [Tooltip("Texto 'Pressione qualquer tecla para continuar'")]
        public string pakText = "Press {KEY} to continue";
        #endregion

        #region Resources
        [Tooltip("Grupo do canvas principal")]
        public CanvasGroup canvasGroup;

        [Tooltip("Grupo do canvas de fundo")]
        public CanvasGroup backgroundCanvasGroup;

        [Tooltip("Grupo do canvas de conteúdo")]
        public CanvasGroup contentCanvasGroup;

        [Tooltip("Grupo do canvas 'Pressione qualquer tecla'")]
        public CanvasGroup pakCanvasGroup;

        [Tooltip("Objeto de status")]
        public TextMeshProUGUI statusObj;

        [Tooltip("Objeto de título")]
        public TextMeshProUGUI titleObj;

        [Tooltip("Objeto de descrição")]
        public TextMeshProUGUI descriptionObj;

        [Tooltip("Barra de progresso")]
        public Slider progressBar;

        [Tooltip("Imagem de fundo da tela de carregamento")]
        public Sprite backgroundImage;

        [Tooltip("Transform do spinner")]
        public Transform spinnerParent;
        #endregion

        #region Hints
        [SerializeField]
        [Tooltip("Ativar dicas aleatórias")]
        private bool enableRandomHints = true;

        [Tooltip("Objeto de texto para dicas")]
        public TextMeshProUGUI hintsText;

        [Tooltip("Mudar dica com temporizador")]
        public bool changeHintWithTimer;

        [Range(1, 60)]
        [Tooltip("Tempo de exibição de cada dica")]
        public float hintTimerValue = 5;

        [TextArea]
        [Tooltip("Lista de dicas a serem exibidas")]
        public List<string> hintList = new List<string>();
        int currentHintIndex = 0;
        #endregion

        #region Background Images
        [SerializeField]
        [Tooltip("Ativar troca de imagens de fundo aleatórias")]
        private bool enableRandomImages = true;

        [Tooltip("Objeto de imagem de fundo")]
        public Image imageObject;

        [Tooltip("Lista de imagens de fundo")]
        public List<Sprite> imageList = new List<Sprite>();

        [Tooltip("Mudar imagem com temporizador")]
        public bool changeImageWithTimer;

        [Tooltip("Tempo de exibição de cada imagem de fundo")]
        public float imageTimerValue = 5;

        [Range(0.1f, 10)]
        [Tooltip("Velocidade da transição de imagem")]
        public float imageFadingSpeed = 4;
        int currentImageIndex = 0;
        #endregion

        #region Press Any Key
        [Tooltip("Texto da mensagem 'Pressione qualquer tecla'")]
        public TextMeshProUGUI pakTextObj;

        [Tooltip("Rótulo do temporizador 'Pressione qualquer tecla'")]
        public TextMeshProUGUI pakCountdownLabel;

        [Tooltip("Slider do temporizador 'Pressione qualquer tecla'")]
        public Slider pakCountdownSlider;

        [Tooltip("Usar uma tecla específica")]
        public bool useSpecificKey = false;

        [Tooltip("Ativar contagem regressiva para 'Pressione qualquer tecla'")]
        public bool useCountdown = true;

        [Tooltip("Esperar entrada do jogador para continuar")]
        public bool waitForPlayerInput = false;

        [Range(1, 30)]
        [Tooltip("Tempo da contagem regressiva")]
        public int pakCountdownTimer = 5;

#if ENABLE_LEGACY_INPUT_MANAGER
        public KeyCode keyCode = KeyCode.Space;
#elif ENABLE_INPUT_SYSTEM
        public InputAction keyCode;
#endif
        #endregion

        #region Virtual Loading
        [Tooltip("Ativar carregamento virtual")]
        public bool enableVirtualLoading = false;

        [Tooltip("Tempo do carregamento virtual")]
        public float virtualLoadingTimer = 5;
        float currentVirtualTime;
        #endregion

        #region Settings
        [SerializeField]
        [Tooltip("Definir Time Scale para 1 durante o carregamento")]
        private bool setTimeScale = true;

        [SerializeField]
        [Tooltip("Ativar 'Pressione qualquer tecla'")]
        private bool enablePressAnyKey = true;

        [SerializeField]
        [Tooltip("Ativar ativação manual da cena")]
        private bool customSceneActivation = false;

        [Range(0.25f, 10)]
        [Tooltip("Velocidade de fade da tela de carregamento")]
        public float fadeSpeed = 4;

        [Range(0.25f, 10)]
        [Tooltip("Velocidade de fade do fundo")]
        public float backgroundFadeSpeed = 2;

        [Range(0.25f, 10)]
        [Tooltip("Velocidade de fade do conteúdo")]
        public float contentFadeSpeed = 2;
        #endregion

        // O restante do código continua da mesma forma
    }
}
