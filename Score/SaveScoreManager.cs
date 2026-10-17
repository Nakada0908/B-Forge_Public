using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveScoreManager : MonoBehaviour
{
    public static SaveScoreManager Instance;

    //最高スコア10個を保持するリスト
    private List<int> highScores = new List<int>();
    //保存するスコアの最大数
    private const int MaxHighScores = 10;
    //保存先のファイルパス
    private string filePath;


    void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        filePath = Application.persistentDataPath + "/highscores.txt";
        LoadScores();
        for (int i = 0; i < highScores.Count; i++)
        {
            //インデックスに1を足して順位として表示
            Debug.Log("Rank " + (i + 1) + ": " + highScores[i]);
        }
    }

    public void SaveScore()
    {
        int score = ScoreManager.Instance.nowScore;
        CheckAndSaveScore(score);
    }

    private void LoadScores()
    {
        if (File.Exists(filePath))
        {
            //読み込んだ文字列配列をfor文で整数に変換して追加する処理に変更
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            //リストを初期化
            highScores.Clear();
            //for文で1行ずつ整数に変換してリストに追加
            for (int i = 0; i < lines.Count; i++)
            {
                highScores.Add(int.Parse(lines[i]));
            }
            //読み込み後もソートと件数制限を適用
            SortAndTrimScores();
        }
    }

    private void SortAndTrimScores()
    {
        //リストの昇順ソートした後に反転させる
        highScores.Sort();
        highScores.Reverse();
        //10個を超えている場合は超過分を削除
        if (highScores.Count > MaxHighScores)
        {
            highScores.RemoveRange(MaxHighScores, highScores.Count - MaxHighScores);
        }
    }

    private void CheckAndSaveScore(int newScore)
    {
        highScores.Add(newScore);
        //降順にソートして上位10個を保持
        SortAndTrimScores();
        //テキストファイルに保存
        WriteScoresToFile();
    }

    private void WriteScoresToFile()
    {
        string[] lines = new string[highScores.Count];
        //for文で要素を1個ずつ文字列に変換して配列へ
        for (int i = 0; i < highScores.Count; i++)
        {
            lines[i] = highScores[i].ToString();
        }
        //改行区切りでファイルに保存
        File.WriteAllLines(filePath, lines);
    }

    public List<int> GetHighScores()
    {
        return new List<int>(highScores);
    }
}