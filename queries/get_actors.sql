select
	t.id
	,t.tag
	,ti.[index]
from taggings ti
join tags t
	on ti.tag_id = t.id
where ti.metadata_item_id = 1
	and t.tag_type = 6
order by ti.[index];
