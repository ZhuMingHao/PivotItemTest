using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PivotItemTest
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions { get => questions; set => questions = value; }

        public MainPageViewModel()
        {
            questions = new ObservableCollection<Question>();
            this.Questions.Add(new Question { Text = "Hello This Nico !", QuestionNumber = "1", RightAnswer = new Answer { Text = "Nico" } });
            this.Questions.Add(new Question { Text = "Hello This Sunteen !", QuestionNumber = "2", RightAnswer = new Answer { Text = "Sunteen" } });
            this.Questions.Add(new Question { Text = "Hello This Lidong !", QuestionNumber = "3", RightAnswer = new Answer { Text = "Lidong" } });
        }
    } 
    public class Question : INotifyPropertyChanged
    {
        public string QuestionNumber { get; set; }
        public string Text { get; set; }
        public Answer RightAnswer { get; set; }
        public ObservableCollection<Answer> Answers { get => answers; set => answers = value; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Answer> answers;
        private Question CurrentQuestion;
        public event PropertyChangedEventHandler PropertyChanged;
        private Answer selectItem;
        private Answer Tem;
        public Answer SelectItem
        {            
            get
            {
                return selectItem;
            }
            set
            {
                selectItem = value;
                
                if (selectItem.Text == CurrentQuestion.RightAnswer.Text)
                {
                    selectItem.IsRightFlag = true;
                    IsCheck = true;
                }
                else
                {
                    selectItem.IsRightFlag = false;            
                    if(Tem == null)
                    {
                        return;
                    }
                    else
                    {
                        Tem.IsRightFlag = false;
                    }                 
                    IsCheck = false;
                }
                OnPropertyChanged();
                Tem = selectItem;
            }
        }
        private bool isCheck;
        public bool IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                isCheck = value;
                OnPropertyChanged();
            }
        }
        public ICommand ItemCommand
        {
            get
            {
                return new CommadEventHandler<Question>((item) => ItemClick(item));
            }
        }
        private void ItemClick(Question item)
        {
            this.CurrentQuestion = item;
        }
        public Question()
        {
            answers = new ObservableCollection<Answer>();
            Answers.Add(new Answer { Text = "Lidong"});
            Answers.Add(new Answer { Text = "Nico" });
            Answers.Add(new Answer { Text = "Sunteen" });
            Answers.Add(new Answer { Text = "Who ?" });
        }
    }
    public class Answer : INotifyPropertyChanged
    {
        public string Text { get; set; }
        private bool isRigntFlag = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public Answer()
        {
          
        }
        public bool IsRightFlag
        {
            get
            {
                return isRigntFlag;
            }
            set
            {
                isRigntFlag = value;
                OnPropertyChanged();
            }
        }
    }
    class CommadEventHandler<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<T> action;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action((T)parameter);
        }
        public CommadEventHandler(Action<T> action)
        {
            this.action = action;

        }
    }
}
