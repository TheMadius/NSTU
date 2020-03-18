#include "ContrlObject.h"

ContrlObject::ContrlObject(QObject *parent): QObject(parent), QGraphicsItem()
{
     this->m_step = 5;
     m_direction = 0;
     this->m_stepNPS = 0;
     this->m_stepBack = 0;
     this->move = false;
}

void ContrlObject::contrlPlayer()
{

    if(GetAsyncKeyState(Qt::Key_W) ||
          GetAsyncKeyState(Qt::Key_A) ||
          GetAsyncKeyState(Qt::Key_S) ||
          GetAsyncKeyState(Qt::Key_D))
       {
            move = true;

            if(GetAsyncKeyState(Qt::Key_A))
            {
               this->setX(this->x() - m_step);

                if (!this->scene()->collidingItems(this).isEmpty())
                {
                    this->setX(this->x() + m_step);
                }
               m_direction = 3;
            }

            if(GetAsyncKeyState(Qt::Key_D))
            {
                this->setX(this->x() + m_step);

                if (!this->scene()->collidingItems(this).isEmpty())
                {
                    this->setX(this->x() - m_step);
                }

                m_direction = 2;
            }

            if(GetAsyncKeyState(Qt::Key_W))
            {
                 this->setY(this->y() - m_step);

                if (!this->scene()->collidingItems(this).isEmpty())
                {
                    this->setY(this->y() + m_step);
                }

                m_direction = 0;
            }

            if(GetAsyncKeyState(Qt::Key_S))
            {
                this->setY(this->y() + m_step);

                if (!this->scene()->collidingItems(this).isEmpty())
                {
                    this->setY(this->y() - m_step);
                }

                m_direction = 1;
            }

        }
    else
    {
        move = false;
    }
}

void ContrlObject::contrlNPS()
{
    srand(clock()*(long)(this->x() - this->y()));

    if (m_stepNPS == 0)
    {
          m_stepNPS =  rand() % 100;
          m_directionOfTravel = rand() % 4;
    }
        switch (m_directionOfTravel)
        {
            case 3:
                 {
                    this->setX(this->x() - m_step);

                    if(!scene()->collidingItems(this).isEmpty())
                    {
                         this->setX(this->x() + (m_step + m_stepBack));
                         m_directionOfTravel = (m_directionOfTravel + 1) % 4;
                         m_stepBack+=7;
                         break;
                    }
                    m_stepBack = 0;
                    break;
                 }
            case 2:
                 {
                    this->setX(this->x() + m_step);

                    if(!scene()->collidingItems(this).isEmpty())
                    {
                        this->setX(this->x() - (m_step + m_stepBack));
                        m_directionOfTravel = (m_directionOfTravel + 1) % 4;
                        m_stepBack++;
                        break;
                    }
                    m_stepBack = 0;
                    break;
                 }
            case 0:
                 {
                   this->setY(this->y() - m_step);

                   if(!scene()->collidingItems(this).isEmpty())
                   {
                        this->setY(this->y() + (m_step + m_stepBack));
                        m_directionOfTravel = (m_directionOfTravel + 1) % 4;
                        m_stepBack++;
                        break;
                   }
                    m_stepBack = 0;
                    break;
                 }
            case 1:
                 {
                    this->setY(this->y() + m_step);

                    if(!scene()->collidingItems(this).isEmpty())
                    {
                        this->setY(this->y() - (m_step + m_stepBack));
                        m_directionOfTravel = (m_directionOfTravel + 1) % 4;
                        m_stepBack++;
                        break;
                    }
                    m_stepBack = 0;
                    break;
                 }

        }

     m_stepNPS--;
}
