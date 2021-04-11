#ifndef GAMEWINDOW_H
#define GAMEWINDOW_H
#define START_COUNT_HUMAN 100
#define START_COUNT_DOCTOR 30
#include <iostream>
#include <QMainWindow>
#include <QWidget>
#include <QGraphicsScene>
#include <QShortcut>
#include <QTimer>
#include "human.h"
#include <QKeyEvent>
#include "camera.h"
#include "customscene.h"
#include "doctor.h"

using namespace std;

QT_BEGIN_NAMESPACE
namespace Ui
{
class GameWindow;
}
QT_END_NAMESPACE

class GameWindow : public QMainWindow
{
    Q_OBJECT

public:
    GameWindow(QWidget *parent = nullptr);
    void addNPS(qreal x,qreal y);
    void addDoctor(qreal x,qreal y);
    void init();
    void Restart();

   ~GameWindow();

signals:
    public slots:
        void gameTimerPositeon();
        void slotTarget(QPointF point);
private:
    static QList<Human *> NPS;
    Ui::GameWindow *ui;
    CustomScene *scene;
    QTimer *timer;
    ContrlObject *player;
    Camera *cam;
    Disease **arrDisease;
    QMetaObject::Connection myConnect;

};
#endif // GAMEWINDOW_H
