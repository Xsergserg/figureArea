SELECT products.product_name, categories.category_name FROM products 
LEFT JOIN categories_products ON products.ID=categories_products.product_id 
LEFT JOIN categories ON categories_products.categorie_id = categories.ID
