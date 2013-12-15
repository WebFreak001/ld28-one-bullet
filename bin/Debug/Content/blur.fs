uniform sampler2D texture;
uniform float rad;

void main()
{
	vec2 x = vec2(rad, 0.0);
	vec2 y = vec2(0.0, rad);

	vec4 pixel = texture2D(texture, gl_TexCoord[0].xy) +
				 texture2D(texture, gl_TexCoord[0].xy + x) +
				 texture2D(texture, gl_TexCoord[0].xy - x) +
				 texture2D(texture, gl_TexCoord[0].xy + y) +
				 texture2D(texture, gl_TexCoord[0].xy + y + x) +
				 texture2D(texture, gl_TexCoord[0].xy + y - x) +
				 texture2D(texture, gl_TexCoord[0].xy - y) +
				 texture2D(texture, gl_TexCoord[0].xy - y + x) +
				 texture2D(texture, gl_TexCoord[0].xy - y - x) +
				 texture2D(texture, gl_TexCoord[0].xy - 2 * y) +
				 texture2D(texture, gl_TexCoord[0].xy - 2 * y + x) +
				 texture2D(texture, gl_TexCoord[0].xy - 2 * y - x) +
				 texture2D(texture, gl_TexCoord[0].xy + 2 * y) +
				 texture2D(texture, gl_TexCoord[0].xy + 2 * y + x) +
				 texture2D(texture, gl_TexCoord[0].xy + 2 * y - x) + 
				 texture2D(texture, gl_TexCoord[0].xy + 2 * x) +
				 texture2D(texture, gl_TexCoord[0].xy + 2 * x + y) +
				 texture2D(texture, gl_TexCoord[0].xy + 2 * x - y) + 
				 texture2D(texture, gl_TexCoord[0].xy - 2 * x) +
				 texture2D(texture, gl_TexCoord[0].xy - 2 * x + y) +
				 texture2D(texture, gl_TexCoord[0].xy - 2 * x - y);
	
	vec4 color = 1 - (pixel / 21.0);
	color.rgb *= -1;
	color *= 0.985;
	gl_FragColor = color;
}
