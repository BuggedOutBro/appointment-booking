create table if not exists  sales_managers (
    id serial primary key not null,
    name varchar(250) not null,
    languages varchar(100)[],
    products varchar(100)[],
    customer_ratings varchar(100)[]
);

create table if not exists slots (
    id serial primary key not null,
    start_date timestamptz not null,
    end_date timestamptz not null,
    booked boolean not null default false,
    sales_manager_id int not null references sales_managers(Id)
);


DROP INDEX IF EXISTS idx_slots_combined;
CREATE INDEX idx_slots_combined 
ON slots (sales_manager_id, start_date, end_date)
WHERE booked = FALSE;

DROP INDEX IF EXISTS idx_sales_managers_languages;
CREATE INDEX idx_sales_managers_languages 
ON sales_managers USING gin (languages);

DROP INDEX IF EXISTS idx_sales_managers_products;
CREATE INDEX idx_sales_managers_products 
ON sales_managers USING gin (products);

DROP INDEX IF EXISTS idx_sales_managers_customer_ratings;
CREATE INDEX idx_sales_managers_customer_ratings 
ON sales_managers USING gin (customer_ratings);


insert into sales_managers (name, languages, products, customer_ratings) values ('Seller 1', '{"German"}', '{"SolarPanels"}', '{"Bronze"}');
insert into sales_managers (name, languages, products, customer_ratings) values ('Seller 2', '{"German", "English"}', '{"SolarPanels", "Heatpumps"}', '{"Gold","Silver","Bronze"}');
insert into sales_managers (name, languages, products, customer_ratings) values ('Seller 3', '{"German", "English"}', '{"Heatpumps"}', '{"Gold","Silver","Bronze"}');

insert into slots (sales_manager_id, booked, start_date, end_date) values (1, false, '2024-05-03T10:30Z', '2024-05-03T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (1, true,  '2024-05-03T11:00Z', '2024-05-03T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (1, false, '2024-05-03T11:30Z', '2024-05-03T12:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, false, '2024-05-03T10:30Z', '2024-05-03T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, false, '2024-05-03T11:00Z', '2024-05-03T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, false, '2024-05-03T11:30Z', '2024-05-03T12:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, true,  '2024-05-03T10:30Z', '2024-05-03T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, false, '2024-05-03T11:00Z', '2024-05-03T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, false, '2024-05-03T11:30Z', '2024-05-03T12:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (1, false, '2024-05-04T10:30Z', '2024-05-04T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (1, false, '2024-05-04T11:00Z', '2024-05-04T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (1, true,  '2024-05-04T11:30Z', '2024-05-04T12:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, true,  '2024-05-04T10:30Z', '2024-05-04T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, false, '2024-05-04T11:00Z', '2024-05-04T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (2, true,  '2024-05-04T11:30Z', '2024-05-04T12:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, true,  '2024-05-04T10:30Z', '2024-05-04T11:30Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, false, '2024-05-04T11:00Z', '2024-05-04T12:00Z');
insert into slots (sales_manager_id, booked, start_date, end_date) values (3, false, '2024-05-04T11:30Z', '2024-05-04T12:30Z');


