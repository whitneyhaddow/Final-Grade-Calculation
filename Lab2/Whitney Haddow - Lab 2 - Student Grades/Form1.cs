using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whitney_Haddow___Lab_2___Student_Grades

    /***************************************************
     * This form calculates final grade for a student
     * based on grades from various class components
     * Created October 2014 by Whitney Haddow
    ****************************************************/

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


       //closes application when clicked
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       //clears all fields
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQuiz1.Text = "";
            txtQuiz2.Text = "";
            txtQuiz3.Text = "";
            txtMidterm.Text = "";
            txtFinalExam.Text = "";
            lblFinalGrade.Text = "";
            lblFinalLetter.Text = "";
            lblQuizDropped.Text = "";
            txtQuiz1.Focus(); //places focus on first text box

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //declare variables
            decimal quiz1; //grade for quiz 1
            decimal quiz2; //grade for quiz 2
            decimal quiz3; //grade for quiz 3
            string quizDropped; //quiz being dropped (lowest grade of the 3)
            decimal midterm; //grade for midterm
            decimal finalExam; //grade for final exam
            decimal finalGrade; //final grade once all grades are compiled
            char finalLetter; //final letter grade corresponding to the final grade
            decimal quizPercent1; //percent contribution of first remaining quiz
            decimal quizPercent2; //percent contribution of second remaining quiz
            decimal midtermPercent; //percent contribution of midterm
            decimal finalExamPercent; //percent contribution of final exam

            //get inputs and convert to a percent in terms of decimal (by dividing by 100)
            quiz1 = Convert.ToDecimal(txtQuiz1.Text) / 100;
            quiz2 = Convert.ToDecimal(txtQuiz2.Text) / 100;
            quiz3 = Convert.ToDecimal(txtQuiz3.Text) / 100;
            midterm = Convert.ToDecimal(txtMidterm.Text) / 100;
            finalExam = Convert.ToDecimal(txtFinalExam.Text) / 100;

            //drop lowest quiz and calculate percent contribution of remaining quizes
            if (quiz1 < quiz2 & quiz1 < quiz3)
            {
                quizDropped = "Quiz 1";
                quizPercent1 = quiz2 * 0.15m;
                quizPercent2 = quiz3 * 0.15m;
            }
            else if (quiz2 < quiz1 & quiz2 < quiz3)
            {
                quizDropped = "Quiz 2";
                quizPercent1 = quiz1 * 0.15m;
                quizPercent2 = quiz3 * 0.15m;
            }
            else
            {
                quizDropped = "Quiz 3";
                quizPercent1 = quiz1 * 0.15m;
                quizPercent2 = quiz2 * 0.15m;
            }

            //calculate percent contribution of midterm and final exam
            midtermPercent = midterm * 0.30m;
            finalExamPercent = finalExam * 0.40m;
            
            //calculate final percent grade
            finalGrade = (quizPercent1 + quizPercent2 + midtermPercent + finalExamPercent);

            //determine corresponding letter grade
            if (finalGrade >= 0.9m)
                finalLetter = 'A';
            else if (finalGrade >= 0.8m && finalGrade < 0.9m)
                finalLetter = 'B';
            else if (finalGrade >= 0.7m && finalGrade < 0.8m)
                finalLetter = 'C';
            else if (finalGrade >= 0.6m && finalGrade < 0.7m)
                finalLetter = 'D';
            else
                finalLetter = 'F';

            //display results
            lblFinalLetter.Text = Convert.ToString(finalLetter);
            lblFinalGrade.Text = finalGrade.ToString("p0");
            lblQuizDropped.Text = quizDropped + " was dropped!";

            
        }
    }
}
