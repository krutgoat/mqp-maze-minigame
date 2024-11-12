extends CharacterBody2D

var speed = 100
var direction = 1
var velocity = Vector2

func _physics_process(delta):
	if is_on_wall():
		direction *= -1
		$AnimatedSprite2D.flip_h = not $AnimatedSprite2D.flip_h
	
	velocity.x = speed * direction
	
	move_and_slide()
