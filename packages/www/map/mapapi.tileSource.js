

(function(window, undefined){
	window['mapapi'] = window['mapapi'] || {};
	var
		document     = window['document'],
		mapapi       = window['mapapi']
	;

	var tileSource = function(options){
		var
			obj        = this,
			objopts    = (obj['options'] = {}),
			options    = options || {},
			copyright  = options['copyright'],
			label      = options['label'],
			minZoom    = options['minZoom'] || 0,
			maxZoom    = options['maxZoom'] || 0,
			bgColor    = options['backgroundColor'] || '#000000',
			width      = Math.max(1, options['width'] || 256),
			height     = Math.max(1, options['height'] || width),
			mimeType   = options['mimeType'] || 'image/jpeg',
			opacity    = Math.max(0, Math.min(1, options['opacity'] || 1))
		;

		if(!copyright){
			throw 'tile source copyright not specified';
		}else if(!label){
			throw 'tile source label not specified';
		}

		obj['size']                = new mapapi['size'](width, height)

		objopts['copyright']       = copyright;
		objopts['label']           = label;
		objopts['minZoom']         = Math.max(minZoom, 0);
		objopts['maxZoom']         = Math.max(objopts['minZoom'] + 1, maxZoom);
		objopts['backgroundColor'] = bgColor;
		objopts['mimeType']        = mimeType;
		objopts['opacity']         = opacity;
	}

	tileSource.prototype.getTileURL = function(pos, zoom){
		return 'data:text/plain,';
	}

	mapapi['tileSource'] = tileSource;
	mapapi['tileSource'].prototype['getTileURL'] = tileSource.prototype.getTileURL;
})(window);