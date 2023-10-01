using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Notes_Spawner : MonoBehaviour
{
    // BGA Player
    public VideoPlayer BGA_player;

    // File Data
    string pathFolder = @"C:\Users\USER\Documents\Rythm";
    string FileName = "IfWithYou_Lena_mingky_ver_4k.txt";

    // Note Objects
    public GameObject Note; // Prefab Note
    public List<ButtonContoller> Lines = new List<ButtonContoller>(); // Run In Lines

    // Read Notes Data
    public string[] readData;
    public List<DTO_Note> completeNoteData = new List<DTO_Note>();
    public int Look_idx = 0;

    // UI
    public TextMeshProUGUI TXTP_NoteSpeed;

    // Note Spwan
    public float Timer = 0;
    public bool IsSpawn = false;

    // Note Position
    public float CheckPosY = 200;
    public float LengthPosY = 1000;
    public float RunTime = 5.0f;

    // Note Speed
    public float NoteSpeed = 1.0f; // Now Note Speed
    public int IsPlusLast = 0; // -1 : Minus Mode // 0 : No // 1 : Plus Mode
    public bool PressingPlus = false; // F : uping // T : pressing
    public bool PressingMinus = false; // F : uping // T : pressing
    public float NoteSpeedTimer = 0.0f; // Note Speed Update Timer

    void Start()
    {
        string path = pathFolder + "/" + FileName;
        readData = ReadTxt(path).Split(',');
        foreach (string note in readData)
            completeNoteData.Add(new DTO_Note(note));
    }

    void Update()
    {
        // Note Speed Controll
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PressingMinus = true;
            IsPlusLast = -1;
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            PressingMinus = false;
            if (PressingPlus)
                IsPlusLast = 1;
            else
                IsPlusLast = 0;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PressingPlus = true;
            IsPlusLast = 1;
        }
        else if (Input.GetKeyUp(KeyCode.F2))
        {
            PressingPlus = false;
            if (PressingMinus)
                IsPlusLast = -1;
            else
                IsPlusLast = 0;
        }
        NoteSpeedTimer += Time.deltaTime;
        if (NoteSpeedTimer > 0.15f)
        {
            NoteSpeed = Mathf.Clamp(NoteSpeed + IsPlusLast / 10.0f, 1.0f, 20.0f);
            NoteSpeedTimer = 0;
        }

        // Start Game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BGA_player.Play();
            Timer = 0;
            IsSpawn = true;
        }

        if (TXTP_NoteSpeed != null)
            TXTP_NoteSpeed.text = string.Format("{0:0.0} ", NoteSpeed); ;

        // Spawn Note Check
        if (IsSpawn && Look_idx < completeNoteData.Count && Note != null)
        {
            for (int i = Look_idx; i < completeNoteData.Count; i++)
            {
                if (completeNoteData[i].SpwanTime <= Timer * 1000)
                {
                    foreach (int line in completeNoteData[i].spwanLines)
                    {
                        GameObject newNote = Instantiate(Note);
                        newNote.gameObject.name = string.Concat("Note : ", Look_idx, " // SpwanTime : ", completeNoteData[i].SpwanTime);
                        newNote.transform.parent = this.transform;
                        newNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(Lines[line].RT.anchoredPosition.x, 1300);
                        Note_Controller nc = newNote.GetComponent<Note_Controller>();
                        Lines[line].Notes.Add(newNote.GetComponent<RectTransform>());
                        nc.StartDown(CheckPosY, this, RunTime);
                    }
                    Look_idx += 1;
                }
                else
                    break;
            }
        }

        Timer += Time.deltaTime;
    }

    string ReadTxt(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();
        }

        else
            value = "파일이 없습니다.";

        return value;
    }

    void WriteTxt(string filePath, string message)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        FileStream fileStream
            = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        writer.WriteLine(message);
        writer.Close();
    }
}
