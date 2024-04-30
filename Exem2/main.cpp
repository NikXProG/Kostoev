#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>
#include <cmath>
using namespace std;
bool isBetween(double value, double a, double b, double epsilon) {
    // Проверяем, что value находится в интервале от a до b, с учетом эпсилона
    return (value + epsilon >= a && value - epsilon <= b) || (value - epsilon <= a && value + epsilon >= b);
}

void read_file(const string& ,vector<double>&);
int main() {
    try{
        vector<double> arr;
        read_file("C:/Users/tiger/CLionProjects/untitled2/file_stream.txt", arr);
        sort(arr.begin(), arr.end());
        double front_el = arr.front();
        double back_el = arr.back();
        double R =  back_el - front_el; // different between min element and max element
        int n = arr.size(); // count interval
        int k = 1+3.322*log10(n); // optimal count interval

        double sum_n = 0;
        double sum_relative_density = 0;

        double h = R/k;
        double sum = front_el;
        double epsilon =1e-12;

        do{

            double count = 0;
            sum += h;
            cout << "[" << sum-h << " " << sum << "] ";
            double x = sum - h/2;
            cout << x << " ";
            for(double & it : arr) {
                if( ( (it  > sum-h ) && (it  < sum ) ) || (( abs(it - (sum-h) ) <= epsilon ) || ( abs(it - sum ) <= epsilon )) ){ count++; }

            }
            cout << count << " ";
            double frequency_density = ((double) count) / h;
            double relative_density = count / n ;
            double relative_frequency_density = relative_density / h;

            cout << frequency_density <<  " " <<  relative_density<<  " " << relative_frequency_density <<  endl;
            sum_n += count;
            sum_relative_density += relative_density;
        } while (  sum  < back_el );
        cout << sum_n << " ";
        cout << sum_relative_density << " ";
        system("python C:/Users/tiger/CLionProjects/untitled2/main.py");
    }

    catch(invalid_argument& e){
        cerr << e.what() << endl;
        return -1;
    }

    return 0;
}

void read_file(const string& file_name, vector<double>& vector_in){
    string line;
    ifstream file(file_name);
    if ( file.is_open() )
    {

        while (getline(file, line))
        {

            size_t pos;
            do{
                pos = line.find(',');
                vector_in.push_back(stod(line.substr(0,pos)));
                line.erase(0, pos+1);
            }while(pos != string::npos);

        }
    }
    else{
        throw invalid_argument("Error file: Not found file or not allocate of memory");
    }
    file.close();
}
void output_writing_in_file(const string& file_name, vector<double>& vector_in){
    string line;
    ifstream file(file_name);
    if ( file.is_open() )
    {

        while (getline(file, line))
        {

            size_t pos;
            do{
                pos = line.find(',');
                vector_in.push_back(stod(line.substr(0,pos)));
                line.erase(0, pos+1);
            }while(pos != string::npos);

        }
    }
    else{
        throw invalid_argument("Error file: Not found file or not allocate of memory");
    }
    file.close();
}