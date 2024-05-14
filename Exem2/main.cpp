#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>
#include <cmath>

using namespace std;

void output_interval_series(const string&, vector<double>&);
void output_writing_in_file(const string&);
void read_file(const string& ,vector<double>&);

int main() {

    try{
        vector<double> arr;

        read_file("./file_stream.txt", arr);
        sort(arr.begin(), arr.end()); 
        output_interval_series("./data.txt", arr);
        system("python main.py");
        return 0;
    }
    catch(const runtime_error& e){
        cerr << e.what() << endl;
        return -1;
    }

}

void read_file(const string& filepath, vector<double>& vector_in){
    string line;
    ifstream inf(filepath);
    if (!inf.is_open()){
        throw runtime_error("Error file: Not found file or not allocate of memory");
    }

    while (getline(inf, line))
    {
        size_t pos;
        do{
            pos = line.find(',');
            vector_in.push_back(stod(line.substr(0,pos)));
            line.erase(0, pos+1);
        }while(pos != string::npos);
    }
    inf.close();
}

void output_interval_series(const string& filepath, vector<double>& arr){
    string line;
    ofstream out(filepath);
    
    if (!out.is_open()){
        throw runtime_error("Error file: Not found file or not allocate of memory");
    }
    out << "intervals xi ni ni/h wi wi/h" << endl;

    double front_el = arr.front(); // самый наименьший член коллекции
    double back_el = arr.back(); // самый наибольший член коллекции

    double R =  back_el - front_el; // размах вариации (длина общего интервала)
    int n = arr.size(); // количество интервалов
    int k = 1+3.322*log10(n); // формула Стерджеса ( оптимальное разбиение интервалов )

    double sum_n = 0;
    double sum_relative_density = 0;

    double h = R/k; // длина одного интервала

    double sum = front_el;
    double epsilon =1e-12;

    do{

        double count = 0;
        sum += h;

        // интервалы
        string interval = "[ " + to_string(sum-h) + ' ' +  to_string(sum) + " ]";

        // расчитывание середин (xi)
        string x = to_string(sum - h/2);

        // количество чисел принадлежащих данному интервалу
        for(double & it : arr) {
            if( ( (it  > sum-h ) && (it  < sum ) ) || (( abs(it - (sum-h) ) <= epsilon ) || ( abs(it - sum ) <= epsilon )) ){ count++; }
        }

        // плотность частот 
        string frequency_density =  to_string(count / h);
        
        //　относительные частоты
        string relative_density =  to_string( ((double) count / n ));
        
        // плотность относительных частот 
        string relative_frequency_density =  to_string(  ( (double) count / n) / h );
    
        out << interval  + ' ' +   x + ' ' + to_string(count) + ' ' + frequency_density + ' ' +  relative_density  + ' ' +   relative_frequency_density << endl;
        sum_n += count;
        sum_relative_density += (count / n);

    } while (  sum  < back_el );

    //out <<  to_string( sum_n) + ' ' + to_string(round(sum_relative_density)) << endl;
    out.close(); 
}
