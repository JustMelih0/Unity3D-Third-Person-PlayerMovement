# Unity3D-Third-Person-PlayerMovement
Unity3D Third Person Character Movement Template

# Tr
Karakteri yeni bir sahneye atmak istiyorsanız tek yapmanız gereken oluşturduğunuz sahnedeki
kamerayı kaldırmak ve "Prefab" klasöründen karakteri ve kamerayı sahneye sürüklemek.
Ardından karakterde ki Scripte kamera objesini sürükleyip bırakın ve aynı şekilde kameranın script 
bileşenine de karakter objesini sürükleyip bırakın.

Not: Karakterin yürüme ve koşma hızını değiştirmek isterseniz aynı şekilde Animator/Locomotion kısmından yürüme ve koşma
animasyonlarınının parametrelerinide eşdeğer yapmalısınız. Aksi takdirde, animasyonda kayma yaşanabilir.

# En
If you want to put the character in a new scene, all you have to do is remove the camera from the scene you created and drag the character and camera from the "Prefab" folder to the scene. Then drag and drop the camera object to the script on the character and drag and drop the character object to the script component of the camera in the same way.

Note: If you want to change the walking and running speed of the character, you should also make the parameters of the walking and running animations equal in the Animator/Locomotion section. Otherwise, the animation may slip.