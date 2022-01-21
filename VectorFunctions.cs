using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFunctions : MonoBehaviour
{
    public static Vector3 rotatePoint(Vector3 point, Vector3 pivot, float angleZ) {
        //calculate directional vector between point and pivot
        Vector3 direction = point - pivot;
        Vector3 angles = new Vector3(0, 0, angleZ);

        //rotate directional vector by angles
        direction = Quaternion.Euler(angles) * direction;

        //now use new directional vector to calculate final point
        point = direction + pivot;
        return point;
    }

    public static Vector2 rotatePoint(Vector2 point, Vector2 pivot, float angleZ) {
        //calculate directional vector between point and pivot
        Vector2 direction = point - pivot;
        Vector2 angles = new Vector3(0, 0, angleZ);

        //rotate directional vector by angles
        direction = Quaternion.Euler(angles) * direction;

        //now use new directional vector to calculate final point
        point = direction + pivot;
        return point;
    }

    public static float clockwiseZAngle(Vector3 start, Vector3 end) {
        //for calculating the angle bewteen 2 directional vectors on the XY plane (2D)

        //signed angle always returns a number between -180 and 180
        float signedAngle = Vector3.SignedAngle(start, end, Vector3.forward);

        if (signedAngle < 0) {
            signedAngle += 360;
        }

        return signedAngle;
    }

    public static float clockwiseZAngle(Vector2 start, Vector2 end) {
        //for calculating the angle bewteen 2 directional vectors on the XY plane (2D)

        //signed angle always returns a number between -180 and 180
        float signedAngle = Vector2.SignedAngle(start, end);

        if (signedAngle < 0) {
            signedAngle += 360;
        }

        return signedAngle;
    }

    public static Vector3 scalePoint(Vector3 point, Vector3 origin, float scale) {
        //calculate directional vector between point and origin
        Vector3 direction = point - origin;

        //normalize it to length 1, then scale it by scale
        direction = direction.normalized * scale;

        //calculate new point based off new directional vector
        point = direction + origin;
        return point;
    }

    public static float calculateZAngleToPoint(Vector3 position, Vector3 target, Vector3 currentDirection) {
        Vector2 location = new Vector2(position.x, position.y);
        Vector2 targetLocation = new Vector2(target.x, target.y);
        Vector2 targetDirection = targetLocation - location;
        Vector2 direction = new Vector2(currentDirection.x, currentDirection.y);

        float angle = Vector2.SignedAngle(direction, targetDirection);
        return angle;
    }
}
