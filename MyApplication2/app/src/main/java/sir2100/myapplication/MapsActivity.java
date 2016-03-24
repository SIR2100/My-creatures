package sir2100.myapplication;

import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.view.View;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

public class MapsActivity extends FragmentActivity {

    private GoogleMap mMap; // Might be null if Google Play services APK is not available.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);
        setUpMapIfNeeded();
    }

    @Override
    protected void onResume() {
        super.onResume();
        setUpMapIfNeeded();
    }

    private void setUpMapIfNeeded() {
        if (mMap == null) {
            mMap = ((SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map)).getMap();
            mMap.setMyLocationEnabled(true);
            mMap.getUiSettings().setZoomControlsEnabled(true);
            if (mMap != null) {
                setUpMap();
            }
        }
    }

    public void onClickTest(View view) {
        if (mMap.getMapType() == GoogleMap.MAP_TYPE_NORMAL) {
            mMap.setMapType(GoogleMap.MAP_TYPE_TERRAIN);
        } else if (mMap.getMapType() == GoogleMap.MAP_TYPE_TERRAIN) {
            mMap.setMapType(GoogleMap.MAP_TYPE_SATELLITE);
        } else if (mMap.getMapType() == GoogleMap.MAP_TYPE_SATELLITE) {
            mMap.setMapType(GoogleMap.MAP_TYPE_HYBRID);
        } else if (mMap.getMapType() == GoogleMap.MAP_TYPE_HYBRID) {
            mMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        }
    }

    private void setUpMap() {
        LatLng mapCenter = new LatLng(47.21665, 39.6286);

        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(mapCenter, 1));

        mMap.addMarker(new MarkerOptions().position(new LatLng(47.21665, 39.6286)).title("South Federal University: MMCS"));

        CameraPosition cameraPosition = CameraPosition.builder()
                .target(mapCenter)
                .zoom(16)
                .bearing(-180)
                .build();

        // Animate the change in camera view over 2 seconds
        mMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition),
                2000, null);
    }
}
