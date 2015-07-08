using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using LottoWinner.Model;

namespace LottoWinner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LottoNumber _last;

        private LottoNumber _forelast;

        private List<LottoNumber> _generatedNumbers;

        private List<int> _availableNumbers;

        private List<Tripel<int>> _generatedTripels = new List<Tripel<int>>();

        public MainWindow()
        {
            InitializeComponent();

            //HistoricalNumbers numbers = new HistoricalNumbers();
            //numbers.Load();

            _last = new LottoNumber();
            _forelast = new LottoNumber();
            _generatedNumbers = new List<LottoNumber>();

            _availableNumbers = new List<int>();
            InitAvailableNumbers();
        }

        private void InitAvailableNumbers()
        {
            _availableNumbers.Clear();
            for (int i = 0; i < 49; i++)
            {
                _availableNumbers.Add(i + 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _forelast.Numbers[0] = int.Parse(Forelast1.Text);
            _forelast.Numbers[1] = int.Parse(Forelast2.Text);
            _forelast.Numbers[2] = int.Parse(Forelast3.Text);
            _forelast.Numbers[3] = int.Parse(Forelast4.Text);
            _forelast.Numbers[4] = int.Parse(Forelast5.Text);
            _forelast.Numbers[5] = int.Parse(Forelast6.Text);

            _forelast.SuperNumber = int.Parse(ForeLastSuper.Text);

            _last.Numbers[0] = int.Parse(Last1.Text);
            _last.Numbers[1] = int.Parse(Last2.Text);
            _last.Numbers[2] = int.Parse(Last3.Text);
            _last.Numbers[3] = int.Parse(Last4.Text);
            _last.Numbers[4] = int.Parse(Last5.Text);
            _last.Numbers[5] = int.Parse(Last6.Text);

            _last.SuperNumber = int.Parse(LastSuper.Text);

            int numberOfLotte = int.Parse(NumberOfLotto.Text);

            Random random = new Random();
            int i = 0;
            while (i < numberOfLotte)
            {
                InitAvailableNumbers();
                LottoNumber newNumber = new LottoNumber();
                for (int j = 0; j < 6; j++)
                {
                    int index = random.Next(0, 49 - j);
                    newNumber.Numbers[j] = _availableNumbers[index];
                    _availableNumbers.RemoveAt(index);
                }
                if (!Checker1(newNumber))
                {
                    continue;
                }
                if (!Checker2(newNumber))
                {
                    continue;
                }

                newNumber.SuperNumber = random.Next(0, 10);
                _generatedNumbers.Add(newNumber);
                i++;
            }
            StringBuilder output = new StringBuilder();
            int counter = 1;
            foreach (var lottoNumber in _generatedNumbers)
            {
                Array.Sort(lottoNumber.Numbers);
                output.Append("Number " + counter + ":");
                foreach (var number in lottoNumber.Numbers)
                {
                    output.Append("\t" + number);
                }
                output.Append("\t Super: " + lottoNumber.SuperNumber);
                output.AppendLine();
                output.AppendLine();
                counter++;
            }
            OutputTB.Text = output.ToString();
        }

        private bool Checker1(LottoNumber number)
        {
            bool isValid = !(HistoricalNumbers.GetNumberOfSameNumbers(number, _forelast) > 2);

            if (HistoricalNumbers.GetNumberOfSameNumbers(number, _last) > 2)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool Checker2(LottoNumber number)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    for (int k = j + 1; k < 6; k++)
                    {
                        Tripel<int> tripel = new Tripel<int>(number.Numbers[i], number.Numbers[j], number.Numbers[k]);
                        if (_generatedTripels.Any(item => item.Equals(tripel)))
                        {
                            return false;
                        }
                        _generatedTripels.Add(tripel);

                    }

                }
            }
            return true;
        }
    }
}
