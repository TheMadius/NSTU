#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

std::string getByte(std::string str)
{
    std::string res = "";
    for(int i = 0; i < str.size();++i)
    {
        std::string temp = "";
        temp += ConvertCRC::byteToStr((uint8_t)str[i]);
        for(int i = temp.size(); i < 8; ++i)
        {
            temp = "0" + temp;
        }
        res += temp;
    }
    return res;
}

std::string getByteInCheck8(std::string str,int sizeCRC)
{
    int i;
    std::string res = "",crc;
    for(i = 0; i < (str.size() - 1);++i)
    {
        std::string temp = "";
        temp += ConvertCRC::byteToStr((uint8_t)str[i]);

        for(int i = temp.size(); i < 8; ++i)
        {
            temp = "0" + temp;
        }
        res += temp;
    }
    crc = ConvertCRC::byteToStr((uint8_t)str[i]);
    for(int i = crc.size(); i < sizeCRC - 1; ++i)
    {
        crc = "0"+ crc;
    }
    return res + crc;
}
std::string getByteInCheck16(std::string str,int sizeCRC)
{
    int i;
    std::string res = "", crc1, crc2;
    for(i = 0; i < (str.size() - 2);++i)
    {
        std::string temp = "";
        temp += ConvertCRC::byteToStr((uint8_t)str[i]);

        for(int i = temp.size(); i < 8; ++i)
        {
            temp = "0" + temp;
        }
        res += temp;
    }
    crc1 = ConvertCRC::byteToStr((uint8_t)str[i]);
    crc2 = ConvertCRC::byteToStr((uint8_t)str[i+1]);

    for(int i = crc1.size(); i < sizeCRC - 9; ++i)
        crc1 = "0"+ crc1;

    for(int i = crc2.size(); i < 8; ++i)
        crc2 = "0"+ crc2;

    return res + crc1 + crc2;
}
void MainWindow::on_pushButton_clicked()
{
    std::string path_file = ui->lineEdit->text().toStdString();

    std::fstream ifs(path_file);
    if(ifs.is_open())
    {
        std::string s, byte, crc_sts, remainder,temp;
        uint8_t crc;
        s.assign((std::istreambuf_iterator<char>(ifs.rdbuf())), std::istreambuf_iterator<char>());
        ifs.close();
        byte = getByte(s);
        crc = ConvertCRC::CRC8__table(s);
        crc_sts = ConvertCRC::byteToStr(crc);
        remainder = ConvertCRC::CRC__get(byte,crc_sts);
        s += ConvertCRC::StrToByte8(remainder);

        ifs.open(path_file);
        ifs << s;
        ifs.close();
        ui->textBrowser->setText("CEC8 : " + QString(crc_sts.c_str()));
    }
    else
    {
        ui->textBrowser->setText("Error!!!!");
    }
}


void MainWindow::on_pushButton_3_clicked()
{
    std::string path_file = ui->lineEdit->text().toStdString();

    std::ifstream ifs(path_file);
    if(ifs.is_open())
    {
        std::string s, byte, crc_sts, remainder;
        crc_sts = ui->lineEdit_2->text().toStdString();
        s.assign((std::istreambuf_iterator<char>(ifs.rdbuf())), std::istreambuf_iterator<char>());
        ifs.close();
        byte = getByteInCheck8(s,crc_sts.size());
        remainder = ConvertCRC::CRC__remainder(byte,crc_sts);
        if(ConvertCRC::StrToByte8(remainder) == 0)
            ui->textBrowser_2->setText("Файл целый");
        else
            ui->textBrowser_2->setText("Ошибка!!! Файл битый");
    }
    else
    {
        ui->textBrowser->setText("Error!!!!");
    }
}

void MainWindow::on_pushButton_2_clicked()
{
    std::string path_file = ui->lineEdit->text().toStdString();

    std::fstream ifs(path_file);
    if(ifs.is_open())
    {
        std::string s, byte, crc_sts, remainder,temp;
        uint16_t crc;
        s.assign((std::istreambuf_iterator<char>(ifs.rdbuf())), std::istreambuf_iterator<char>());
        ifs.close();
        byte = getByte(s);
        crc = ConvertCRC::CRC16__table(s);
        crc_sts = ConvertCRC::byteToStr(crc);
        remainder = ConvertCRC::CRC__get(byte,crc_sts);

        s += ConvertCRC::StrTo2Char(remainder);

        ifs.open(path_file,std::ios_base::out);
        ifs << s;
        ifs.close();

        ui->textBrowser->setText("CEC16 : " + QString(crc_sts.c_str()));
    }
    else
    {
        ui->textBrowser->setText("Error!!!!");
    }
}

void MainWindow::on_pushButton_4_clicked()
{
    std::string path_file = ui->lineEdit->text().toStdString();

    std::ifstream ifs(path_file);
    if(ifs.is_open())
    {
        std::string s, byte, crc_sts, remainder;
        crc_sts = ui->lineEdit_2->text().toStdString();
        s.assign((std::istreambuf_iterator<char>(ifs.rdbuf())), std::istreambuf_iterator<char>());
        ifs.close();
        byte = getByteInCheck16(s,crc_sts.size());
        remainder = ConvertCRC::CRC__remainder(byte,crc_sts);
        if(ConvertCRC::StrToByte16(remainder) == 0)
            ui->textBrowser_2->setText("Файл целый");
        else
            ui->textBrowser_2->setText("Ошибка!!! Файл битый");
    }
    else
    {
        ui->textBrowser->setText("Error!!!!");
    }
}
