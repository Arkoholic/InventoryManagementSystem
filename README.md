# InventoryManagementSystem<br />
Interim Assessment for DCIT 318<br />
Arkoh Ebenezer<br />
10813547<br />
Shoprite Mangement App<br />

Assumptions:

Login is impossible if username and/or password fields are incorrect. <br />
All passwords are blocked when being typed and has a checkBox to override. <br />
the app takes only one admin and as many attendants. <br />
The admin has more priviledges than the attendant.<br />
The admin has access to the Products, Categories, User, Transaction and customer. <br />
If a product's quantity = 1, then a message constantly reminds the admin to restock.<br />
The attendant has access to only the customer and the transaction per the requirements. <br />
The attendant selects the customer, the quantity and the product is equivalent to the attendant using a barcode scanner and a customer identifier.<br />
SearchBoxes make identifying products and transactions easier.<br />
The admin can view ungoing transactions.<br />
The attendant can delete all transactions to indicate the end of sales for the day. <br />
The attendant is given 1000 as change. The attendant cannot edit this for trust reasons.<br />
The total sales can also be seen in real-time.<br />
The profit or loss can be seen. substracting the change from the total sales.<br />

Downfalls: <br />
Inability to hash passwords in the database
