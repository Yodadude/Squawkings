alter table users
alter column isgravatar drop default,
alter column isgravatar type boolean  using (case isgravatar when 1 then true else false end),
alter column isgravatar set default false;
