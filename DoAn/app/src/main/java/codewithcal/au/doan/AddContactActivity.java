package codewithcal.au.doan;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import codewithcal.au.doan.objects.Contact;
import codewithcal.au.doan.sqlite.DatabaseHandler;

public class AddContactActivity extends AppCompatActivity {
    private Button btnSave, btnCancel;
    private EditText txtName, txtPhone;
    private DatabaseHandler db;
    private String flag;
    private int idEdit;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.layout_add);
        db = new DatabaseHandler(this);
        init();
        //lay ve gia tri ben activity cha
        Intent i = getIntent();

        flag = i.getStringExtra("flag");
        if (flag.equals("1")) {//neu la nut sua thi day gia tri can sua len giao dien
            Contact c = (Contact) i.getSerializableExtra("contact");
            txtName.setText(c.getName());
            txtPhone.setText(c.getPhoneNumber());
            idEdit = c.getID();
        }

        //bat su kien nhan nut
        btnCancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String name = txtName.getText().toString();
                String phone = txtPhone.getText().toString();
                //add contact
                //Lưu vào sqlite

                if (flag.equals("1")) {

                    //Lưu vào sqlite
                    Contact c = new Contact(idEdit, name, phone);
                    db.updateContact(c);
                    //day tra ve mainactivity
                    Intent i = new Intent();
                    i.putExtra("contact", c);
                    setResult(RESULT_OK, i);
                    finish();
                } else {
                    //lay ve ten, id, phonenumber
                    try {
                        //Lưu vào sqlite
                        Contact c = new Contact(1, name, phone);
//goi ham addcontactl
                        long newid = db.addContact(c);

                        c.setID((int) newid);
                        //cập nhật trên giao diện
//                Contact c = new Contact(id,name,phone);
                        //day tra ve mainactivity
                        Intent i = new Intent();
                        i.putExtra("contact", c);
                        setResult(RESULT_OK, i);
                        finish();
                    } catch (Exception ex) {
                        Log.e("Ham them moi contact", ex.getMessage());
                    }
                }
            }
        });
    }

    public void init() {
        getSupportActionBar().setTitle(getString(R.string.addcontacttitle));
        btnSave = (Button) findViewById(R.id.btnSave);
        btnCancel = (Button) findViewById(R.id.btnCancel);
        txtName = (EditText) findViewById(R.id.txtName);
        txtPhone = (EditText) findViewById(R.id.txtPhone);
    }
}
