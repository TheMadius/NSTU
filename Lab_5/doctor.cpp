#include "doctor.h"

Doctor::Doctor():Human(Female)
{
    for (int j = 0 ; j < COUNT_ANIMATION; j++)
            for (int i = 0; i < COUNT_IMG; i++)
               delete m_animation[j][i];

    eHeal = 5;

    this->m_ability = false;

    this->timerHeal = new QTimer();

    timerHeal->start(3000);

    connectHeal = connect(timerHeal,&QTimer::timeout,this,&Doctor::Healing);

    initDoc();

}

void Doctor::Healing()
{
    srand(clock()*(long)(this->x() - this->y()));

    QRectF zona(this->x()-50,this->y()-50,100,100);

    foreach (auto x,  scene()->items(zona))
    {

      Human* per = dynamic_cast<Human*>(x);

      if(per == nullptr)
          continue;

      foreach (auto var, per->getlistDisease())
      {
        if (rand()/(float)RAND_MAX <= var.getprobabilityCure())
        {
            per->delDisease(var);
        }
      }

      if (x == this)
          continue;

      per->damage(100);
     }
}

void Doctor::initDoc()
{
    //up
    this->m_animation[0][0] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_07.png");
    this->m_animation[0][1] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_08.png");
    this->m_animation[0][2] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_09.png");
    this->m_animation[0][3] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_10.png");
    this->m_animation[0][4] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_11.png");
    this->m_animation[0][5] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_12.png");
    this->m_animation[0][6] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_08.png");
    //down
    this->m_animation[1][0] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_01.png");
    this->m_animation[1][1] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_02.png");
    this->m_animation[1][2] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_03.png");
    this->m_animation[1][3] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_04.png");
    this->m_animation[1][4] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_05.png");
    this->m_animation[1][5] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_06.png");
    this->m_animation[1][6] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_02.png");
    //right
    this->m_animation[2][0] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_19.png");
    this->m_animation[2][1] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_20.png");
    this->m_animation[2][2] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_21.png");
    this->m_animation[2][3] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_22.png");
    this->m_animation[2][4] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_23.png");
    this->m_animation[2][5] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_24.png");
    this->m_animation[2][6] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_20.png");
    //left
    this->m_animation[3][0] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_13.png");
    this->m_animation[3][1] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_14.png");
    this->m_animation[3][2] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_14.png");
    this->m_animation[3][3] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_15.png");
    this->m_animation[3][4] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_16.png");
    this->m_animation[3][5] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_17.png");
    this->m_animation[3][6] = new QPixmap(":/resourse/doctor/images/Sprite_old_woman_14.png");

}

Doctor::~Doctor()
{
    disconnect(connectHeal);
    delete this->timerHeal;
}
