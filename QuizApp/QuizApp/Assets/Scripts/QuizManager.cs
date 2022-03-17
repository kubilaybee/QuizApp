using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text QuestionText;
    public Button ULButton;
    public Button URButton;
    public Button DLButton;
    public Button DRButton;
    public Button ResetButton;
    public Button ShowQuestionButton;

    [Header("Questions")]
    public List<QuestionUX> quizData = new List<QuestionUX>();

    // App elements
    private List<Question> quiz = new List<Question>();
    // question check controller
    private bool checkTheAnswer = true;
    // question count
    private int questionCount = 0;
    // check correctly choice
    private string correctlyChoice;

    private void Start()
    {
        fixTheQuestionList();

    }

    public void resetBtn()
    {
        questionCount = 0;
    }

    public void showTheQuestion()
    {
        List<Button> tempBtn = addChoice();
        foreach (Button btn in tempBtn)
        {
            btn.GetComponent<Image>().color = Color.white;
        }
        if (checkTheAnswer)
        {
            // check the quiz questions
            if (questionCount < quiz.Count || questionCount == 0)
            {
                showTheQuestionUI(quiz[questionCount]);
                checkTheAnswer = false;
            }
        }
    }

    // check the answer -btn image color change
    private void checkTheAnswerUI()
    {
        // correctly choice
        foreach (Answer ans in quiz[questionCount].choices)
        {
            // add the correctly choice
            if (ans.correct)
            {
                correctlyChoice = ans.answer;
            }
        }

        List<Button> choices = addChoice();
        
        // change btn background color
        foreach(Button btn in choices)
        {
            if (btn.GetComponentInChildren<Text>().text == correctlyChoice)
            {
                btn.GetComponent<Image>().color = Color.green;
            }
            else
            {
                btn.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public List<Button> addChoice()
    {
        List<Button> tempList = new List<Button>();
        tempList.Add(ULButton);
        tempList.Add(URButton);
        tempList.Add(DLButton);
        tempList.Add(DRButton);
        return tempList;
    }

    // onBtnClicked
    public void btnOnclick()
    {
        checkTheAnswerUI();
        checkTheAnswer = true;
        questionCount++;
    }

    private void Update()
    {
    }
    // +++
    public void showTheQuestionUI(Question ques)
    {
        QuestionText.text = ques.question;

        ULButton.GetComponentInChildren<Text>().text = ques.choices[0].answer;
        URButton.GetComponentInChildren<Text>().text = ques.choices[1].answer;
        DLButton.GetComponentInChildren<Text>().text = ques.choices[2].answer;
        DRButton.GetComponentInChildren<Text>().text = ques.choices[3].answer;
    }

    // UX quiz elements turn the main quiz ++
    public void fixTheQuestionList()
    {
        // enter the first quiz question
        foreach (QuestionUX tempData in quizData)
        {
            Question tempQuestion = new Question();
            // question choice list
            List<Answer> tempChoices = new List<Answer>() {
                new Answer(){answer=tempData.choice1.answer,correct = tempData.choice1.correct},
                new Answer(){answer=tempData.choice2.answer,correct = tempData.choice2.correct},
                new Answer(){answer=tempData.choice3.answer,correct = tempData.choice3.correct},
                new Answer(){answer=tempData.choice4.answer,correct = tempData.choice4.correct},
            };
            // add the question
            tempQuestion.question = tempData.question;
            // add the answers
            tempQuestion.choices = tempChoices;

            //add the quiz List
            quiz.Add(tempQuestion);
        }
    }

}
// +++
[Serializable]
public class Question
{
    public string question;
    public List<Answer> choices = new List<Answer>();
}

// +++
[Serializable]
public class Answer
{
    public string answer;
    public bool correct;
}

// +++
[Serializable]
public class QuestionUX
{
    public string question;
    public Answer choice1;
    public Answer choice2;
    public Answer choice3;
    public Answer choice4;

}
