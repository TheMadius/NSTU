#ifndef CUSTOMSCENE_H
#define CUSTOMSCENE_H

#include <QObject>
#include <QGraphicsScene>
#include <QGraphicsSceneMouseEvent>
#include <QDebug>

class CustomScene : public QGraphicsScene
{
   Q_OBJECT
public:
    explicit CustomScene(QObject *parent = nullptr);

signals:
    void signalTargetCoordinate(QPointF point);

public slots:

private:
        void mousePressEvent(QGraphicsSceneMouseEvent *event);
};

#endif // CUSTOMSCENE_H
