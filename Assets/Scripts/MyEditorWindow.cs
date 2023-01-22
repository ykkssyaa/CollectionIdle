using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class ItemDatabase : EditorWindow
{
    private Sprite m_DefaultItemIcon;

    // ��������� ������ ��������� ������
    private static List<EggAsset> m_ItemDatabase = new List<EggAsset>();
    private VisualElement m_ItemsTab;
    private static VisualTreeAsset m_ItemRowTemplate;
    private ListView m_ItemListView;
    private float m_ItemHeight = 40;

    // ��������� �������� ��������
    private ScrollView m_DetailSection;
    private VisualElement m_LargeDisplayIcon;
    private EggAsset m_activeItem;

    [MenuItem("WUG/Item Database")]
    public static void Init()
    {
        ItemDatabase wnd = GetWindow<ItemDatabase>();
        wnd.titleContent = new GUIContent("Item Database");
        Vector2 size = new Vector2(800, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }

    public void CreateGUI()
    {
        // ����������� uxml ���������
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            ("Assets/WUG/Editor/ItemDatabase.uxml");

        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);

        // ����������� uss ���������
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>
            ("Assets/WUG/Editor/ItemDatabase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        //Import the ListView Item Template
        m_ItemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WUG/Editor/EggRowTemplate.uxml");
        m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/WUG/Sprites/UnknownIcon.png", typeof(Sprite));

        m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath(
            "Assets/WUG/Sprites/UnknownIcon.png", typeof(Sprite));

        // �������� ���� ��������� ��������� 
        LoadAllItems();

        // �������� � ������ � ���������
        m_ItemsTab = rootVisualElement.Q<VisualElement>("ItemsTab");
        GenerateListView();

        // 
        m_DetailSection = rootVisualElement.Q<ScrollView>("ScrollView_Details");
        m_DetailSection.style.visibility = Visibility.Hidden;
        m_LargeDisplayIcon = m_DetailSection.Q<VisualElement>("Icon");

        // ������ ����������
        rootVisualElement.Q<Button>("Btn_AddItem").clicked += AddItem_OnClick;

        // ������ ��������
        rootVisualElement.Q<Button>("Btn_DeleteItem").clicked += DeleteItem_OnClick;

        // ��������� ���������� ���������� � ��������
        m_DetailSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            m_activeItem.eggName = evt.newValue;
            m_ItemListView.Rebuild();
        });

        m_DetailSection.Q<ObjectField>("IconPicker").RegisterValueChangedCallback(evt =>
            {
                Sprite newSprite = evt.newValue as Sprite;
                m_activeItem.icon = newSprite == null ? m_DefaultItemIcon : newSprite;
                m_LargeDisplayIcon.style.backgroundImage = newSprite ==
                null ? m_DefaultItemIcon.texture : newSprite.texture;
                m_ItemListView.Rebuild();
            });
    }

    // �������� ��������� ��� ������
    private void LoadAllItems()
    {
        m_ItemDatabase.Clear();
        string[] allPaths = Directory.GetFiles("Assets/Data", "*.asset",
            SearchOption.AllDirectories);

        foreach (string path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            m_ItemDatabase.Add((EggAsset)AssetDatabase.LoadAssetAtPath(cleanedPath,
                typeof(EggAsset)));
        }
    }

    // ��������� ������ 
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => m_ItemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage =
                m_ItemDatabase[i] == null ? m_DefaultItemIcon.texture :
                m_ItemDatabase[i].icon.texture;
            e.Q<Label>("Name").text = m_ItemDatabase[i].eggName;
        };
        m_ItemListView = new ListView(m_ItemDatabase, 35, makeItem, bindItem);
        m_ItemListView.selectionType = SelectionType.Single;
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight;
        m_ItemsTab.Add(m_ItemListView);

        m_ItemListView.onSelectionChange += ListView_onSelectionChange;
    }

    // ���������� ������� �� ������� � ������
    private void ListView_onSelectionChange(IEnumerable<object> selectedItems)
    {
        m_activeItem = (EggAsset)selectedItems.First();
        SerializedObject so = new SerializedObject(m_activeItem);
        m_DetailSection.Bind(so);
        if (m_activeItem.icon != null)
        {
            m_LargeDisplayIcon.style.backgroundImage = m_activeItem.icon.texture;
        }
        m_DetailSection.style.visibility = Visibility.Visible;
    }

    // ���������� ������� �� ������ ��������
    private void AddItem_OnClick()
    {
        //Create an instance of the scriptable object and set the default parameters
        EggAsset newItem = CreateInstance<EggAsset>();
        newItem.eggName = $"New Item";
        newItem.icon = m_DefaultItemIcon;
        //Create the asset, using the unique ID for the name
        AssetDatabase.CreateAsset(newItem, $"Assets/Data/{newItem.ID}.asset");
        //Add it to the item list
        m_ItemDatabase.Add(newItem);
        //Refresh the ListView so everything is redrawn again
        m_ItemListView.Rebuild();
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight;
    }

    // ���������� ������� �� ������ ��������
    private void DeleteItem_OnClick()
    {
        //Get the path of the fie and delete it through AssetDatabase
        string path = AssetDatabase.GetAssetPath(m_activeItem);
        AssetDatabase.DeleteAsset(path);
        //Purge the reference from the list and refresh the ListView
        m_ItemDatabase.Remove(m_activeItem);
        m_ItemListView.Rebuild();
        //Nothing is selected, so hide the details section
        m_DetailSection.style.visibility = Visibility.Hidden;
    }
}
