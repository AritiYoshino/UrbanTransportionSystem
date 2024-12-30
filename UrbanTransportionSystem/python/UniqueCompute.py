# -*- coding: utf-8 -*-
import arcpy
# 设置工作空间，将路径替换为你的地理数据库路径

# 要素类名称
fc = "地铁站"

import arcpy

def remove_duplicates(feature_class_name, field_name):
    # 设置工作空间
    arcpy.env.workspace = "D:\\arcmap\\成都市.gdb"
    # 存储已经出现过的字段值
    seen_values = set()
    with arcpy.da.UpdateCursor(feature_class_name, field_name) as update_cursor:
        for row in update_cursor:
            value = row[0]
            if value in seen_values:
                # 删除当前要素
                update_cursor.deleteRow()
            else:
                # 将该字段值添加到已见集合中
                seen_values.add(value)

if __name__ == "__main__":
    remove_duplicates(fc, "name")
