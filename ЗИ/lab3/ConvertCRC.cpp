#include "ConvertCRC.h"

uint8_t ConvertCRC::CRC8__get_code(std::string data_last)
{
    uint8_t crc = 0xff;
    for(int i = 0; i < data_last.size(); i++ )
    {
        crc ^= data_last[i];
        for (int j = 0; j < 8; j++) {
            if ((crc & 0x80) != 0)
                crc = (uint8_t)((crc << 1) ^ 0x31);
            else
                crc <<= 1;
        }
    }
    return crc;
}

uint8_t ConvertCRC::CRC8__table(std::string data_last)
{
    uint8_t crc = 0xff;
    for(int i = 0; i < data_last.size(); ++i)
        crc = crc8x_table[crc ^ data_last[i]];
    return crc;
}

uint16_t ConvertCRC::CRC16__table(std::string data_last)
{
    uint16_t crc = 0xFFFF;
    for(int i = 0; i < data_last.size(); ++i)
        crc = (crc << 8) ^ Crc16Table[(crc >> 8) ^ data_last[i]];
    return crc;
}


std::string ConvertCRC::CRC__get(std::string data, std::string code)
{
    for (int i = 0; i < code.size() - 1; ++i)
        data += "0";

    return CRC__remainder(data, code);
}

std::string ConvertCRC::CRC__remainder(std::string data, std::string code)
{
    auto xr = [](char a, char b) { return (a == b) ? '0' : '1'; };
    for (int i = data.size(); i >= code.size(); --i)
    {
        if (data[data.size() - i] == '0')
            continue;

        for (int j = 0; j < code.size(); ++j)
            data[data.size() - i + j] = xr(data[data.size() - i + j], code[j]);
    }

    return data.substr(data.size() - code.size() + 1, code.size() - 1);
}

std::string ConvertCRC::byteToStr(uint8_t data)
{
    std::string res = "";
    while (true)
    {
        if (data == 0)
           return res;
        res = ((data & 0x1) ? "1" : "0") + res;
        data >>= 1;
    }
}
std::string ConvertCRC::byteToStr(uint16_t data)
{
    std::string res = "";
    while (true)
    {
        if (data == 0)
            return res;
        res = ((data & 0x1) ? "1" : "0") + res;
        data >>= 1;
    }
}
uint8_t ConvertCRC::StrToByte8(std::string str)
{
    uint8_t res = 0;
    for(int i = 0; i < str.size(); ++i)
    {
        res *= 2;

        if(str[i] == '1')
            res += 1;
    }
    return res;
}
uint16_t ConvertCRC::StrToByte16(std::string str)
{
    uint16_t res = 0;
    for(int i = 0; i < str.size(); ++i)
    {
        res *= 2;

        if(str[i] == '1')
            res += 1;
    }
    return res;
}

std::string ConvertCRC::StrTo2Char(std::string str)
{
    uint8_t res1 = 0;
    uint8_t res2 = 0;
    std::string res = "";

    for(int i = str.size(); i < 16; ++i)
        str = "0" + str;

    for(int i = 0; i < str.size()/2; ++i)
    {
        res1 *= 2;
        res2 *= 2;

        if(str[i] == '1')
            res1 += 1;
        if(str[i+8] == '1')
            res2 += 1;
    }
    res += res1;
    res += res2;
    return res;
}
