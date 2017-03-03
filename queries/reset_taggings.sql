delete from taggings
where id in (
	select ti.id
	from taggings ti
	join tags t
		on ti.tag_id = t.id
		and t.tag_type = 6
	where ti.metadata_item_id = 1
	);

insert into taggings (metadata_item_id,tag_id,[index],created_at)
values
	(1,6,0,datetime(current_timestamp, 'localtime'))
	,(1,7,1,datetime(current_timestamp, 'localtime'));
